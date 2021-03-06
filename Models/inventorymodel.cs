using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntergrationA.Models
{
    public class inventorymodel
    {
        public class inventoryjson
        {
            public int seq { get; set; }
            public string itemno { get; set; }
            public string stockcode { get; set; }
            public string longdescription { get; set; }
            public string ClassificationCode { get; set; }
            public string originmanufacture { get; set; }
            public string referenceprice { get; set; }
            public double? Length { get; set; }
            public double? Width { get; set; }
            public double? Height { get; set; }
            public double? Weight { get; set; }
            public string Specifications { get; set; }
            public string PalletQTY { get; set; }
            public string PrimaryMeasurement { get; set; }
            public string DefaultLocation { get; set; }
            public string WarehouseCode { get; set; }
            public string Volume { get; set; }


   public string ImageURL { get; set; }

            public string EshopDesc { get; set; }

            public decimal? EshopUnitPrice { get; set; }

            public decimal? EshopBundleQty { get; set; }

            public decimal? EshopBundlePrice { get; set; }

            public string MarketplaceDesc { get; set; }

            public decimal? MarketplacePrice { get; set; }

            public decimal? MarketplaceBundleQty { get; set; }

            public string POSDesc { get; set; }

            public decimal? POSBundleQTY { get; set; }

            public decimal? POSBundleQty1 { get; set; }

            public decimal? POSBundlePrice { get; set; }

            public string PackageWeight { get; set; }

            public string PackageLength { get; set; }

            public string PackageWidth { get; set; }

            public string PackageHeight { get; set; }

            public string Eshop { get; set; }

            public string Internal { get; set; }

            public string POS { get; set; }

            public string Marketplace { get; set; }



        }

        public class Page
        {
            public int PageSize { get; set; }
            public int PageCount { get; set; }
            public int RecordCount { get; set; }
            public int CurrPage { get; set; }
        }

        public class InventoryBaseclass
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string Id { get; set; }
            public string Code { get; set; }
            public List<inventoryjson> data { get; set; }
            public Page Page { get; set; }
        }

        public class InventoryBaseclassreturn
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string Id { get; set; }
            public string Code { get; set; }
            public List<Inventory> data { get; set; }
            public Page Page { get; set; }
        }

        public class Inventory
        {
            [Key]
            public int seq { get; set; }

            public string itemno { get; set; }

            public string stockcode { get; set; }

            public string longdescription { get; set; }

            public string ClassificationCodeL1 { get; set; }

            public string ClassificationCodeL2 { get; set; }

            public string ClassificationCodeL3 { get; set; }

            public string originmanufacture { get; set; }

            public string referenceprice { get; set; }

            public string Length { get; set; }

            public string Width { get; set; }

            public string Height { get; set; }

            public string Weight { get; set; }

            public string Specifications { get; set; }

            public string PalletQTY { get; set; }

            public string PrimaryMeasurement { get; set; }

            public string DefaultLocation { get; set; }

            public string WarehouseCode { get; set; }

            public string Volume { get; set; }

            public string ImageURL { get; set; }

            public string EshopDesc { get; set; }

            public decimal? EshopUnitPrice { get; set; }

            public decimal? EshopBundleQty { get; set; }

            public decimal? EshopBundlePrice { get; set; }

            public string MarketplaceDesc { get; set; }

            public decimal? MarketplacePrice { get; set; }

            public decimal? MarketplaceBundleQty { get; set; }

            public string POSDesc { get; set; }

            public decimal? POSBundleQTY { get; set; }

            public decimal? POSBundleQty1 { get; set; }

            public decimal? POSBundlePrice { get; set; }

            public string PackageWeight { get; set; }

            public string PackageLength { get; set; }

            public string PackageWidth { get; set; }

            public string PackageHeight { get; set; }

            public string Eshop { get; set; }

            public string Internal { get; set; }

            public string POS { get; set; }

            public string Marketplace { get; set; }

            public string CreatedBy { get; set; }

            public DateTime? CreatedDate { get; set; }

            public string UpdatedBy { get; set; }

            public DateTime? UpdatedDate { get; set; }

            public bool IsDeleted { get; set; }

        }
    }
}