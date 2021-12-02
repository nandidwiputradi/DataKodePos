using DataKodePosWeb.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Q = DataKodePosWeb.API.Query;

namespace DataKodePosWeb.API
{
    public interface IRepository : IDisposable
    {
        dynamic GetPropKab();
        dynamic GetKecamatan();
        dynamic GetKelurahan();
        dynamic GetKelurahanName();
        dynamic DeleteKelurahan();
    }
    public class Repository : IRepository
    {
        public dynamic GetPropKab()
        {
            dynamic retval = null;

            int pageIndex = int.Parse(RequestData.Data.pageIndex);
            string searchText = RequestData.Data.value;
            string cmdText = Q.qGetPropKab;

            cmdText = string.Format(cmdText, pageIndex, searchText);
            var res = DBHelper.Page<PropKabClass>(cmdText);
            res.PageIndex = pageIndex;
            res.SearchType = RequestData.Data.type;
            retval = res;
            return retval;
        }
        public dynamic GetKecamatan()
        {
            dynamic retval = null;

            int pageIndex = int.Parse(RequestData.Data.pageIndex);
            string searchText = RequestData.Data.value;
            string cmdText = Q.qGetKecamatan;

            cmdText = string.Format(cmdText, pageIndex, searchText);
            var res = DBHelper.Page<KecamatanClass>(cmdText);
            res.PageIndex = pageIndex;
            res.SearchType = RequestData.Data.type;
            retval = res;
            return retval;
        }
        public dynamic GetKelurahan()
        {
            dynamic retval = null;

            int pageIndex = int.Parse(RequestData.Data.pageIndex);
            string searchText = RequestData.Data.value;
            string cmdText = Q.qGetKelurahan;

            cmdText = string.Format(cmdText, pageIndex, searchText);
            var res = DBHelper.Page<KelurahanClass>(cmdText);
            res.PageIndex = pageIndex;
            res.SearchType = RequestData.Data.type;
            retval = res;
            return retval;
        }
        public dynamic GetKelurahanName()
        {
            dynamic retval = null;

            string cmdText = Q.qGetKelurahanById;
            string id = RequestData.Data.value;
            cmdText = string.Format(cmdText, id);

            retval = DBHelper.Scalar(cmdText);

            return retval;
        }
        public dynamic DeleteKelurahan()
        {
            dynamic retval = null;

            string cmdText = Q.qDeleteKelurahan;
            string id = RequestData.Data.value;
            cmdText = string.Format(cmdText, id);

            retval = DBHelper.Execute(cmdText);

            return retval;
        }
        public void Dispose()
        {

        }
    }
}