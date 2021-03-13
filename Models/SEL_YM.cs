using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PrOps.Models
{
   /* public class SEL_YM
    {
        
        public string strUserName { get; set; }
        public string strPagePrinted { get; set; }

        
    }

    public class SEL_YM_Input
    {
        [Required]
        [DataType(DataType.Text)]
        public int? iYear { get; set; }
        [Required]
        public int? iMonth { get; set; }
    }

    public class SEL_YM_View
    {
        public DataClassesPODataContext _POContext { get; set; }
        public SEL_YM_View(DataClassesPODataContext POContext)
        {
            _POContext = POContext;
        }
        public IEnumerable< sp_SEL_YMResult> SEL_YMResult { get; set; }
        public int iYear { get; set; }
        public int iMonth { get; set; }
    }

    public class YM_View : List<YM_View>
    {
        public string strUserName {get; set; }
        public int iPagePrinted {get; set;}
        //public IList<
    }


    public interface IYM 
    {
        IEnumerator<sp_SEL_YMResult> GetYM();
    }*/

    public class SEL_YM 
    {
        private DataClassesPODataContext _dataContext;
        private int _iYear;
        private int _iMonth;
        //private List<YM_View> ym_view;
        //private List<List<YM_View>> ym_view2;


        public SEL_YM(int iYear, int iMonth)
        {
            if (iYear > 0)
            {
                _iYear = iYear;
            }
            else
            {
                _iYear = DateTime.Today.Year;
            }
            if (iMonth > 0)
            {
                _iMonth = iMonth;
            }
            else
            {
                _iMonth = DateTime.Today.Month;
            }
            _dataContext = new DataClassesPODataContext();
           

        }

        public int GetYear()
        {
           
            return _iYear;

        }

        public int GetMonth()
        {

            return _iMonth; 
        }

        /*public YM_View ListAll()
        {
            YM_View ym =(YM_View )_dataContext.sp_SEL_YM(2013, 10).ReturnValue;
            return ym;
        } */
        
        /*
        public IEnumerator<sp_SEL_YMResult> GetYM()
        {
            ISingleResult<sp_SEL_YMResult> getYM = _dataContext.sp_SEL_YM(2013, 10);
            
            return getYM.GetEnumerator();
        } */

        public IEnumerable<sp_SEL_YMResult> GetYM()
        {
            //ISingleResult<sp_SEL_YMResult> getYM = _dataContext.sp_SEL_YM(2013, 10);

            return _dataContext.sp_SEL_YM(_iYear, _iMonth); 
        }

        public IEnumerable<sp_SEL_YM_TOTALResult> GetYMTotal()
        {
            return _dataContext.sp_SEL_YM_TOTAL(_iYear, _iMonth);
        }
        /*
        public IEnumerator<sp_SEL_YMResult> GetEnumerator()
        {
            ISingleResult<sp_SEL_YMResult> getYM = _dataContext.sp_SEL_YM(2013, 10);

            return getYM.GetEnumerator(); 
        }*/
        /*
        public IEnumerator<YM_View> GetEnumerator()
        {
            return ym_view.GetEnumerator();
        }

        IEnumerator<YM_View> IEnumerable<YM_View>.GetEnumerator()
        {
            return ym_view.GetEnumerator();
        }*/
    }
}