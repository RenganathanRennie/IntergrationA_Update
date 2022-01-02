using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntergrationA.Models
{
    public class inventorymodel
    {   public class inventory
        {
            public int seq { get; set; }
            public string itemno { get; set; }
            public string stockcode { get; set; }
            public object longdescription { get; set; }
            public string ClassificationCode { get; set; }
            public object originmanufacture { get; set; }
            public object referenceprice { get; set; }
            public double? Length { get; set; }
            public double? Width { get; set; }
            public double? Height { get; set; }
            public object Weight { get; set; }
            public string Specifications { get; set; }
            public object PalletQTY { get; set; }
            public string PrimaryMeasurement { get; set; }
            public string DefaultLocation { get; set; }
            public string WarehouseCode { get; set; }
            public object Volume { get; set; }
            public string ImageURL { get; set; }
            public object EshopDesc { get; set; }
            public double EshopUnitPrice { get; set; }
            public object EshopBundleQty { get; set; }
            public double? EshopBundlePrice { get; set; }
            public object MarketplaceDesc { get; set; }
            public object MarketplacePrice { get; set; }
            public object MarketplaceBundleQty { get; set; }
            public object POSDesc { get; set; }
            public object POSBundleQTY { get; set; }
            public object POSBundleQty1 { get; set; }
            public object POSBundlePrice { get; set; }
            public object PackageWeight { get; set; }
            public object PackageLength { get; set; }
            public object PackageWidth { get; set; }
            public object PackageHeight { get; set; }
            public string Eshop { get; set; }
            public object Internal { get; set; }
            public object POS { get; set; }
            public object Marketplace { get; set; }
        }

        public class Page
        {
            public int PageSize { get; set; }
            public int PageCount { get; set; }
            public int RecordCount { get; set; }
            public int CurrPage { get; set; }
        }

        public class Root
        {
            public bool Success { get; set; }
            public object Message { get; set; }
            public object Id { get; set; }
            public object Code { get; set; }
            public List<inventory> data { get; set; }
            public Page Page { get; set; }
        }
    }
}