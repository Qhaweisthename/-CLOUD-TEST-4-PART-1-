using Azure;
using Azure.Data.Tables;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cloud_Storage.Models
{
  
        public class Customer : ITableEntity
        {
            [Key]
            public string CustomerId { get; set; } // Use string for partitioning flexibility
            public string Name { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }

            // ITableEntity implementation
            public string PartitionKey { get; set; } // Set this as "Customer"
            public string RowKey { get; set; } // Use CustomerId as RowKey
            public ETag ETag { get; set; }
            public DateTimeOffset? Timestamp { get; set; }
        }

        public class Product : ITableEntity
        {
            [Key]
            public string ProductId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string Category { get; set; }

            // ITableEntity implementation
            public string PartitionKey { get; set; } // Set this as "Product"
            public string RowKey { get; set; } // Use ProductId as RowKey
            public ETag ETag { get; set; }
            public DateTimeOffset? Timestamp { get; set; }
        }



    }
