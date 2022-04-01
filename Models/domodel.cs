using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntergrationA_Update.Models
{
    public class domodel
    {

public class domodelsummary
{
    public OrderHeader doheader {get;set;}
    public IEnumerable<OrderDetails> dodetails {get;set;}
    

}
public class OrderDetails
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public long Id { get; set; }
    [MaxLength(100)]
    [Required(ErrorMessage = "Order No is required")]
    public string OrderNo { get; set; }
    [MaxLength(100)]
    [Required(ErrorMessage = "Product Id is required")]
    public string ProductId { get; set; }
    [MaxLength(500)]
    public string ProductName { get; set; }
    [MaxLength(100)]
    public string UnitId { get; set; }
    [Required(ErrorMessage = "Quantity is required")]
    public decimal Quantity { get; set; }
    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public decimal? DiscountPer { get; set; }
    [Required(ErrorMessage = "Total is required")]
    public decimal Total { get; set; }
    [MaxLength(1000)]
    public string Remarks { get; set; }
    [MaxLength(100)]
    public string GSTType { get; set; }
    public decimal? GSTAmount { get; set; }
    [MaxLength(256)]
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    [MaxLength(256)]
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    [Required(ErrorMessage = "Is Deleted is required")]
    public bool IsDeleted { get; set; }
}

        // public class OrderDetails
        // {

        //     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //     public long Id { get; set; }
        //     [MaxLength(100)]
        //     public string OrderNo { get; set; }
            
        //     [MaxLength(100)]
        //     public string ProductId { get; set; }
        //     [MaxLength(500)]
        //     public string ProductName { get; set; }
        //     [MaxLength(100)]
        //     public string UnitId { get; set; }
        //     public decimal Quantity { get; set; }
        //     public decimal Price { get; set; }
        //     public decimal Discount { get; set; }
        //     public decimal DiscountPer { get; set; }
        //     public decimal Total { get; set; }
        //     [MaxLength(1000)]
        //     public string Remarks { get; set; }
        //     [MaxLength(100)]
        //     public string GSTType { get; set; }
        //     public decimal GSTAmount { get; set; }
        //     [MaxLength(256)]
        //     public string CreatedBy { get; set; }
        //     public DateTime CreatedDate { get; set; }
        //     [MaxLength(256)]
        //     public string UpdatedBy { get; set; }
        //     public DateTime UpdatedDate { get; set; }
        //     public bool IsDeleted { get; set; }
        // }


        // // public class OrderHeader
        // {

        //     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //     public long Id { get; set; }
        //     [MaxLength(500)]
        //     public string OrderType { get; set; }
        //     [MaxLength(500)]

        //     [Key]
        //     public string OrderNo { get; set; }
        //     public DateTime OrderDate { get; set; }
        //     [MaxLength(500)]
        //     public string InvoiceNo { get; set; }
        //     public DateTime InvoiceDate { get; set; }
        //     [MaxLength(500)]
        //     public string DoNo { get; set; }
        //     public DateTime DoDate { get; set; }
        //     [MaxLength(100)]
        //     public string CustomerId { get; set; }
        //     [MaxLength(100)]
        //     public string Customer_FirstName { get; set; }
        //     [MaxLength(100)]
        //     public string Customer_LastName { get; set; }
        //     [MaxLength(500)]

        //     public string ShippingCode { get; set; }

        //     public string ShippingAddress1 { get; set; }
        //     [MaxLength(500)]
        //     public string ShippingAddress2 { get; set; }
        //     [MaxLength(500)]
        //     public string ShippingAddress3 { get; set; }
        //     [MaxLength(100)]
        //     public string ShippingZipcode { get; set; }
        //     [MaxLength(100)]
        //     public string ShippingPhone { get; set; }
        //     public double ShippingCost { get; set; }
        //     [MaxLength(100)]
        //     public string ShippingMode { get; set; }
        //     public double Total { get; set; }
        //     [MaxLength(500)]
        //     public string DiscountType { get; set; }
        //     public double DiscountPer { get; set; }
        //     public double DiscountAmount { get; set; }
        //     public double TotalBillDiscount { get; set; }
        //     public double TotalItemDiscount { get; set; }
        //     public double SubTotal { get; set; }
        //     [MaxLength(100)]
        //     public string GSTType { get; set; }
        //     public double GSTPer { get; set; }
        //     public double GSTAmount { get; set; }
        //     public double NetTotal { get; set; }
        //     [MaxLength(100)]
        //     public string Currency { get; set; }
        //     public double CurrencyRate { get; set; }
        //     public double PaidAmount { get; set; }
        //     public double Due { get; set; }
        //     public double Change { get; set; }
        //     [MaxLength(4000)]
        //     public string Remarks { get; set; }
        //     [MaxLength(100)]
        //     public string PaymentNo { get; set; }
        //     [MaxLength(100)]
        //     public string PaymentType { get; set; }
        //     [MaxLength(100)]
        //     public string PaymentMode { get; set; }
        //     [MaxLength(500)]
        //     public string PaymentNotes { get; set; }
        //     [MaxLength(256)]
        //     public string CreatedBy { get; set; }
        //     public DateTime CreatedDate { get; set; }
        //     [MaxLength(256)]
        //     public string UpdatedBy { get; set; }
        //     public DateTime UpdatedDate { get; set; }
        //     public int IsDeleted { get; set; }
        // }
        
public class OrderHeader
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   
    public long Id { get; set; }
    [MaxLength(500)]
    public string OrderType { get; set; }
    [MaxLength(500)]
    [Required(ErrorMessage = "Order No is required")]
    public string OrderNo { get; set; }
    [Required(ErrorMessage = "Order Date is required")]
    public DateTime? OrderDate { get; set; }
    [MaxLength(500)]
    public string InvoiceNo { get; set; }
    public DateTime? InvoiceDate { get; set; }
    [MaxLength(500)]
    public string DoNo { get; set; }
    public DateTime DoDate { get; set; }
    [MaxLength(100)]
    [Required(ErrorMessage = "Customer Id is required")]
    public string CustomerId { get; set; }
    [MaxLength(100)]
    public string Customer_FirstName { get; set; }
    [MaxLength(100)]
    public string Customer_LastName { get; set; }
    [MaxLength(50)]
    [Required(ErrorMessage = "Shipping Code is required")]
    public string ShippingCode { get; set; }
    [MaxLength(500)]
    public string ShippingAddress1 { get; set; }
    [MaxLength(500)]
    public string ShippingAddress2 { get; set; }
    [MaxLength(500)]
    public string ShippingAddress3 { get; set; }
    [MaxLength(100)]
    public string ShippingZipcode { get; set; }
    [MaxLength(100)]
    public string ShippingPhone { get; set; }
    public double? ShippingCost { get; set; }
    [MaxLength(100)]
    public string ShippingMode { get; set; }
    [Required(ErrorMessage = "Total is required")]
    public double Total { get; set; }
    [MaxLength(500)]
    public string DiscountType { get; set; }
    public double? DiscountPer { get; set; }
    public double? DiscountAmount { get; set; }
    public double? TotalBillDiscount { get; set; }
    public double? TotalItemDiscount { get; set; }
    [Required(ErrorMessage = "Sub Total is required")]
    public double SubTotal { get; set; }
    [MaxLength(100)]
    [Required(ErrorMessage = "GST Type is required")]
    public string GSTType { get; set; }
    [Required(ErrorMessage = "GST Per is required")]
    public double GSTPer { get; set; }
    public double? GSTAmount { get; set; }
    [Required(ErrorMessage = "Net Total is required")]
    public double NetTotal { get; set; }
    [MaxLength(100)]
    public string Currency { get; set; }
    public double? CurrencyRate { get; set; }
    public double? PaidAmount { get; set; }
    public double? Due { get; set; }
    public double? Change { get; set; }
    [MaxLength(4000)]
    public string Remarks { get; set; }
    [MaxLength(100)]
    public string PaymentNo { get; set; }
    [MaxLength(100)]
    public string PaymentType { get; set; }
    [MaxLength(100)]
    public string PaymentMode { get; set; }
    [MaxLength(500)]
    public string PaymentNotes { get; set; }
    [MaxLength(256)]
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    [MaxLength(256)]
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    [Required(ErrorMessage = "Is Deleted is required")]
    public int IsDeleted { get; set; }
}


    }
}