using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using OpenAI;
using System.Diagnostics;

namespace NqlDotNet.DevExpress
{
    public class DevExNqlService : INqlService
    {
        IChatCompletionService? chatService;
        Kernel? sk;

        public DevExNqlService()
        {
            var clientOpenAi = new OpenAIClient(new System.ClientModel.ApiKeyCredential(Environment.GetEnvironmentVariable("OpenAiTestKey")));
            var KernelBuilder = Kernel.CreateBuilder();
            KernelBuilder.AddOpenAIChatCompletion("gpt-4o-mini", clientOpenAi);
            sk = KernelBuilder.Build();
            chatService = sk.GetRequiredService<IChatCompletionService>();
        }
        public async Task<string> NlToCriteria(string Nlq, string Schema, string Doc)
        {
            //KernelArguments arguments = new(new OpenAIPromptExecutionSettings { MaxTokens = 500, Temperature = 0.5 }) { { "Nlq", Nlq }, { "Doc", Doc }, { "Schema", Schema } };// new() { { "topic", "sea" } };
            KernelArguments arguments = new(new OpenAIPromptExecutionSettings { MaxTokens = 500, Temperature = 0.5 });
            //Given the DevExpress Criteria Language syntax documentation and the.
            FunctionResult value = await sk.InvokePromptAsync($"Given the DevExpress Criteria Language syntax documentation {Doc} and JSON schema of classes {Schema}, transform a natural language query #{Nlq}# into a criteria string suitable for querying data,return only the criteria without any extra text or explanation", arguments);
            Debug.WriteLine(value);



            return value.ToString();
        }
        public async Task<string> CriteriaToNl(string Criteria, string Schema, string Doc)
        {
            //KernelArguments arguments = new(new OpenAIPromptExecutionSettings { MaxTokens = 500, Temperature = 0.5 }) { { "Nlq", Nlq }, { "Doc", Doc }, { "Schema", Schema } };// new() { { "topic", "sea" } };
            KernelArguments arguments = new(new OpenAIPromptExecutionSettings { MaxTokens = 500, Temperature = 0.5 });
            //Given the DevExpress Criteria Language syntax documentation and the.
            FunctionResult value = await sk.InvokePromptAsync($"Given the DevExpress Criteria Language syntax documentation {Doc} and JSON schema of classes {Schema}, transform the #{Criteria}# into a natural language,return the answer and explanation how you did it", arguments);
            Debug.WriteLine(value);
            return value.ToString();
        }
    }
}
