using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataKodePosWeb.API
{
    public class RequestData
    {
        public static SearchData Data { get; set; }
    }
    public class SearchData
    {
        public string type { get; set; }
        public string value { get; set; }
        public string pageIndex { get; set; }
        public string pageCount { get; set; }
    }
    public class Paging
    {
        public string SearchType { get; set; }

        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public List<dynamic> Data { get; set; }
    }
    public class CommonResult
    {
        public string SearchType { get; set; }
        public dynamic Data { get; set; }
    }
    public class PropKabClass
    {
        public string No { get; set; }
        public string Propinsi { get; set; }
        public string Kabupaten { get; set; }        
        public string Id { get; set; }
    }
    public class KecamatanClass
    {
        public string No { get; set; }
        public string Kecamatan { get; set; }        
        public string Id { get; set; }
    }
    public class KelurahanClass
    {
        public string No { get; set; }
        public string Kelurahan { get; set; }
        public string Kodepos { get; set; }
        public string Id { get; set; }
    }
}