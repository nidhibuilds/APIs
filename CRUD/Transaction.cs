using System;

namespace CRUD
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } // e.g., Credit, Debit
        public string Description { get; set; }
        public int AccountId { get; set; }
    }
} 