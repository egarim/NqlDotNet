using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;


namespace NqlDotNet.Tests.Xpo
{


    public class Customer : XPObject
    {
        public Customer(Session session) : base(session) { }

     

        public string Name { get; set; }

        [Association("CustomerCategory-Customers")]
        public CustomerCategory Category { get; set; }

        [Association("Address-Customers")]
        public Address Address { get; set; }
    }
    public class Address : XPObject
    {
        public Address(Session session) : base(session) { }

     

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }

        [Association("Address-Customers")]
        public XPCollection<Customer> Customers => GetCollection<Customer>();
    }

    public class InvoiceHeader : XPObject
    {
        public InvoiceHeader(Session session) : base(session) { }

      

        [Association("Customer-InvoiceHeaders")]
        public Customer Customer { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }

        [Association("InvoiceHeader-InvoiceDetails")]
        public XPCollection<InvoiceDetails> InvoiceDetails => GetCollection<InvoiceDetails>();
    }

    public class InvoiceDetails : XPObject
    {
        public InvoiceDetails(Session session) : base(session) { }

       

        [Association("InvoiceHeader-InvoiceDetails")]
        public InvoiceHeader InvoiceHeader { get; set; }

        [Association("Product-InvoiceDetails")]
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class Product : XPObject
    {
        public Product(Session session) : base(session) { }

   

        public string Name { get; set; }

        [Association("ProductCategory-Products")]
        public ProductCategory Category { get; set; }

        [Association("PriceList-Products")]
        public PriceList PriceList { get; set; }

        [Association("Product-InvoiceDetails")]
        public XPCollection<InvoiceDetails> InvoiceDetails => GetCollection<InvoiceDetails>();
    }
    public class CustomerCategory : XPObject
    {
        public CustomerCategory(Session session) : base(session) { }

      

        public string CategoryName { get; set; }

        [Association("CustomerCategory-Customers")]
        public XPCollection<Customer> Customers => GetCollection<Customer>();
    }
    public class ProductCategory : XPLiteObject
    {
        public ProductCategory(Session session) : base(session) { }

        [Key(AutoGenerate = true)]
        public string CategoryID { get; set; }

        public string CategoryName { get; set; }

        [Association("ProductCategory-Products")]
        public XPCollection<Product> Products => GetCollection<Product>();
    }
    public class PriceList : XPLiteObject
    {
        public PriceList(Session session) : base(session) { }

        [Key(AutoGenerate = true)]
        public string PriceListID { get; set; }

        [Association("PriceList-Products")]
        public XPCollection<Product> Products => GetCollection<Product>();

        public decimal Price { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}