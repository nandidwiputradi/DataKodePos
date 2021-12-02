using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataKodePosWeb.API
{
    public class Query
    {
        public static string qGetPropKab = @"
declare @filter nvarchar(max) = '%{1}%'
declare @page int = {0}
declare @fetch int = 10 * (@page-1)
declare @DataCount int = 0
declare @PageCount int = 0
declare @PageMod int = 0

select ROW_NUMBER() OVER(order by propinsi.RegionName, kabupaten.RegionName) No, 
propinsi.RegionName Propinsi, kabupaten.RegionName + ' (' +
(select cast(count(a.ID) as nvarchar(max)) from RegionTable a where a.RegionLevel = 2 and a.ParentID = kabupaten.ID and a.StatusCode='0') + ')'
 Kabupaten, kabupaten.ID Id
into #paging
from RegionTable propinsi
join RegionTable kabupaten on kabupaten.ParentID = propinsi.ID and kabupaten.RegionLevel = 1
where propinsi.RegionLevel = 0
and propinsi.StatusCode = 0
and kabupaten.StatusCode = 0
and (propinsi.RegionName <> '' or propinsi.RegionName <> '')
and (propinsi.RegionName like @filter or kabupaten.RegionName like @filter)

select @DataCount = count(No) from #paging
set @PageMod = iif(@DataCount%10 = 0, 0, 1)
set @PageCount = @DataCount/10 + @PageMod

select *, @PageCount [PageCount] from #paging order by No, Propinsi, Kabupaten
offset @fetch rows fetch next 10 rows only

drop table #paging
";
        public static string qGetKecamatan = @"
declare @filter int = {1}
declare @page int = {0}
declare @fetch int = 10 * (@page-1)
declare @DataCount int = 0
declare @PageCount int = 0
declare @PageMod int = 0

select ROW_NUMBER() OVER(order by kecamatan.RegionName) No, 
kecamatan.RegionName + ' (' +
(select cast(count(a.ID) as nvarchar(max)) from RegionTable a where a.RegionLevel = 3 and a.ParentID = kecamatan.ID and a.StatusCode='0') + ')' Kecamatan, kecamatan.ID Id
into #paging
from RegionTable kecamatan
where kecamatan.RegionLevel = 2
and kecamatan.ParentID = @filter
and kecamatan.StatusCode = 0
and kecamatan.RegionName <> ''

select @DataCount = count(No) from #paging
set @PageMod = iif(@DataCount%10 = 0, 0, 1)
set @PageCount = @DataCount/10 + @PageMod

select *, @PageCount [PageCount] from #paging order by No, Kecamatan
offset @fetch rows fetch next 10 rows only

drop table #paging
";
        public static string qGetKelurahan = @"
declare @filter int = {1}
declare @page int = {0}
declare @fetch int = 10 * (@page-1)
declare @DataCount int = 0
declare @PageCount int = 0
declare @PageMod int = 0

select ROW_NUMBER() OVER(order by kelurahan.RegionName) No, 
kelurahan.RegionName Kelurahan, kelurahan.PostalCode Kodepos, kelurahan.ID Id
into #paging
from RegionTable kelurahan
where kelurahan.RegionLevel = 3
and kelurahan.ParentID = @filter
and kelurahan.StatusCode = 0
and kelurahan.RegionName <> ''

select @DataCount = count(No) from #paging
set @PageMod = iif(@DataCount%10 = 0, 0, 1)
set @PageCount = @DataCount/10 + @PageMod

select *, @PageCount [PageCount] from #paging order by No, Kelurahan
offset @fetch rows fetch next 10 rows only

drop table #paging
";
        public static string qGetKelurahanById = @"
select RegionName from RegionTable
where RegionLevel = 3 and ID = {0}
";
        public static string qDeleteKelurahan = @"
update RegionTable set StatusCode = '1' where RegionLevel = 3 and ID = {0}
";
    }
}