using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using testeSistemaEcommerce.Models;

namespace testeSistemaEcommerce.Repository
{
    public class AccessRepository : BaseRepository
    {
        public static DataSet GravarUser(Usuario usuario)
        {
            BaseRepository baseDao = new BaseRepository();
            baseDao.cleanParameters();

            baseDao.addParameter(new SqlParameter("@Nome", usuario.Nome));
            baseDao.addParameter(new SqlParameter("@Telefone", usuario.Telefone));
            baseDao.addParameter(new SqlParameter("@Email", usuario.Email));
            baseDao.addParameter(new SqlParameter("@Documento", usuario.Documento));
            baseDao.addParameter(new SqlParameter("@TipoPessoa", usuario.TipoPessoa));
            return baseDao.getDataSet("sp_p_GravarUser", CommandType.StoredProcedure);
        }
    }
}