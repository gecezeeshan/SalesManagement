using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagement.Models
{
    public class SaleMaster
    {
        public int SaleMasterId { get; set; }
        public  string Customer { get; set; }
        public DateTime Date { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
 public class SaleMasterReport
    {
        public int SaleMasterId { get; set; }
        public  string Customer { get; set; }
        public DateTime Date { get; set; }
        public int Items { get; set; }
        public decimal Total { get; set; }
        
    }
    public class SaleDetail
    {       
        public int SaleDetailId { get; set; }
        [ForeignKey("SaleMaster")]
        public int SaleMasterId { get; set; }
        public SaleMaster SaleMaster { get; set; }
        public string ItemNo { get; set; } 
        public string ItemName { get; set; }
        public int QTY { get; set; }        
        public decimal Tax { get; set; }
        public decimal Price { get; set; }
    }


    public class SaleFilterReport
    {
        public DateTime? Date { get; set; }

    }
    }