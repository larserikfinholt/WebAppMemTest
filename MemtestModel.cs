namespace WebAppMemTest
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MemtestModel
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
