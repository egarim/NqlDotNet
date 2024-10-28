using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OpenAI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NqlDotNet
{

    public class NqlService
    {
        IChatCompletionService? chatService;
        Kernel? sk;

        public NqlService()
        {
            var clientOpenAi = new OpenAIClient(new System.ClientModel.ApiKeyCredential(Environment.GetEnvironmentVariable("OpenAiTestKey")));
            var KernelBuilder = Kernel.CreateBuilder();
            KernelBuilder.AddOpenAIChatCompletion("gpt-4o-mini", clientOpenAi);
            sk = KernelBuilder.Build();
            chatService = sk.GetRequiredService<IChatCompletionService>();
        }
        public async Task<string> CreateCriteria(string Nlq,string Schema,string Doc)
        {
            //KernelArguments arguments = new(new OpenAIPromptExecutionSettings { MaxTokens = 500, Temperature = 0.5 }) { { "Nlq", Nlq }, { "Doc", Doc }, { "Schema", Schema } };// new() { { "topic", "sea" } };
            KernelArguments arguments = new(new OpenAIPromptExecutionSettings { MaxTokens = 500, Temperature = 0.5 });
            //Given the DevExpress Criteria Language syntax documentation and the.
            FunctionResult value = await sk.InvokePromptAsync($"Given the DevExpress Criteria Language syntax documentation {Doc} and JSON schema of classes {Schema}, transform a natural language query #{Nlq}# into a criteria string suitable for querying data,return only the criteria without any extra text or explanation", arguments);
            Debug.WriteLine(value);
          

        
            return value.ToString();
        }
    }
}
