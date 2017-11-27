using System;
using SubSonic;
using VNS.HIS.DAL;
using System.Transactions;

namespace VNS.Libs
{
    public class ChuyenPhongkham
    {
        public static ActionResult ChuyenPhong(long idKham, string lydoChuyen, DmucDichvukcb objDichvuKcb)
        {
            try
            {
            ActionResult actionResult = ActionResult.Success;
                using (var scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        int sttkham = THU_VIEN_CHUNG.LaySothutuKCB(objDichvuKcb.IdPhongkham);
                        new Update(KcbDangkyKcb.Schema)
                        .Set(KcbDangkyKcb.Columns.IdPhongkham).EqualTo(objDichvuKcb.IdPhongkham)
                        .Set(KcbDangkyKcb.Columns.IdBacsikham).EqualTo(-1)
                        .Set(KcbDangkyKcb.Columns.SttKham).EqualTo(sttkham)
                        .Set(KcbDangkyKcb.Columns.IdDichvuKcb).EqualTo(objDichvuKcb.IdDichvukcb)
                        .Set(KcbDangkyKcb.Columns.IdKieukham).EqualTo(objDichvuKcb.IdKieukham)
                        .Set(KcbDangkyKcb.Columns.TenDichvuKcb).EqualTo(objDichvuKcb.TenDichvukcb.ToUpper())
                        .Set(KcbDangkyKcb.Columns.NgayDangky).EqualTo(globalVariables.SysDate)
                        .Set(KcbDangkyKcb.Columns.NguoiChuyen).EqualTo(globalVariables.UserName)
                        .Set(KcbDangkyKcb.Columns.NgayChuyen).EqualTo(globalVariables.SysDate)
                        .Set(KcbDangkyKcb.Columns.LydoChuyen).EqualTo(lydoChuyen)
                        .Set(KcbDangkyKcb.Columns.TrangthaiChuyen).EqualTo(1)
                        .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(idKham)
                        .Execute();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi chuyển đối tượng:\n"+ex.Message);
                return ActionResult.Exception;
            }
        }
       

      
       
    }
}
