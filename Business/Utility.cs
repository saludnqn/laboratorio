using System;
using System.Security.Cryptography;
using System.Configuration;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;
//using Sql.Data

namespace Business
{
	/// <summary>
	/// Summary description for Utility.
	/// </summary>
	public class Utility
	{
		private PDSAEncryptionType mbytEncryptionType;
		private string mstrOriginalString;
		private string mstrEncryptedString;
		private SymmetricAlgorithm mCSP;

		public enum PDSAEncryptionType : byte
		{
			DES,
			RC2,
			Rijndael,
			TripleDES
		}

		#region " Constructores "

		public Utility()
		{
			mbytEncryptionType = PDSAEncryptionType.DES;

			this.SetEncryptor();
		}

		public Utility(PDSAEncryptionType EncryptionType)
		{
			mbytEncryptionType = EncryptionType;

			this.SetEncryptor();
		}

		public Utility(PDSAEncryptionType EncryptionType, string OriginalString)
		{
			mbytEncryptionType = EncryptionType;
			mstrOriginalString = OriginalString;

			this.SetEncryptor();
		}

		#endregion

		#region " Metodos Anteriores "
        
		public  string EncryptAnterior(string strPass)
		{
			long lngChr;
			string strBuff="";
			string strInput = ConfigurationSettings.AppSettings["encKey"];
			strPass = strPass.ToUpper();

			if(strPass.Length != 0) 
			{
				for(int intCnt=0;intCnt<strInput.Length;intCnt++)
				{
					int intStart = intCnt % strPass.Length; lngChr = Convert.ToInt64(strInput.Substring(intCnt,1)[0]); // [0] for to avoid "" from the input, e.g., "S" ==> 'S'
					// To avoid index is not found exception
					if(intStart == (strPass.Length -1))
						lngChr = lngChr + Convert.ToInt64(strPass.Substring(intStart,1)[0]); // [0] for to avoid "" from the input, e.g., "S" ==> 'S'
					else
						lngChr = lngChr + Convert.ToInt64(strPass.Substring(intStart+1,1)[0]); // [0] for to avoid "" from the input, e.g., "S" ==> 'S' 

					strBuff = strBuff + (char)(lngChr & Convert.ToInt64("0xFF", 16));// AND wif 0xFF
				}
			}
			else
				strBuff = strInput;

			return strBuff;
		}


		public  string DecryptAnterior(string strPass) 
		{
			string strBuff="";
			string strInput = ConfigurationSettings.AppSettings["encKey"];
			long lngChr; strPass = strPass.ToUpper();
			if(strPass.Length != 0)
			{
				for(int intCnt=0;intCnt<strInput.Length;intCnt++)
				{ 
					int intStart = intCnt % strPass.Length;

					lngChr = Convert.ToInt64(strInput.Substring(intCnt,1)[0]);// [0] for to avoid "" from the input, e.g., "S" ==> 'S'

					// To avoid index is not found exception
					if(intStart == (strPass.Length -1))
						lngChr = lngChr - Convert.ToInt64(strPass.Substring(intStart,1)[0]);// [0] for to avoid "" from the input, e.g., "S" ==> 'S'
					else
						lngChr = lngChr - Convert.ToInt64(strPass.Substring(intStart+1,1)[0]); // [0] for to avoid "" from the input, e.g., "S" ==> 'S' 

					strBuff = strBuff + (char)(lngChr & Convert.ToInt64("0xFF", 16));// AND wif 0xFF

				}

			}
			else

				strBuff = strInput;

			return strBuff; 
		}

        public void CargarCombo(object ddlArea, string m_ssql, string v1, string v2)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region " Propiedades Publicas "

        public PDSAEncryptionType EncryptionType
		{
			get {return mbytEncryptionType;}
			set
			{
				if (mbytEncryptionType != value) 
				{
					mbytEncryptionType = value;
					mstrOriginalString = String.Empty;
					mstrEncryptedString = String.Empty;

					this.SetEncryptor();
				}
			}
		}

		public SymmetricAlgorithm CryptoProvider
		{
			get {return mCSP;}
			set {mCSP = value;}
		}

		public string OriginalString
		{
			get {return mstrOriginalString;}
			set {mstrOriginalString = value;}
		}

		public string EncryptedString
		{
			get {return mstrEncryptedString;}
			set {mstrEncryptedString = value;}
		}

		public byte[] key
		{
			get {return mCSP.Key;}
			set {mCSP.Key = value;}
		}

		public string KeyString
		{
			get {return Convert.ToBase64String(mCSP.Key);}
			set {mCSP.Key = Encoding.UTF8.GetBytes(value);}
		}

		public byte[] IV
		{
			get {return mCSP.IV;}
			set {mCSP.IV = value;}
		}

		public string IVString
		{
			get {return Convert.ToBase64String(mCSP.IV);}
			set {mCSP.IV = Encoding.UTF8.GetBytes(value);}
		}

		#endregion

		#region " Metodos de Encriptacion "

		public string Encrypt()
		{
			ICryptoTransform ct;
			MemoryStream ms;
			CryptoStream cs;
			byte[] byt;

			ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);

			byt = Encoding.UTF8.GetBytes(mstrOriginalString);

			ms = new MemoryStream();
			cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
			cs.Write(byt, 0, byt.Length);
			cs.FlushFinalBlock();
			cs.Close();

			mstrEncryptedString = Convert.ToBase64String(ms.ToArray());

			return mstrEncryptedString;
		}

		public string Encrypt(string OriginalString)
		{
			mstrOriginalString = OriginalString;
      
			return this.Encrypt();
		}

		public string Encrypt(string OriginalString, PDSAEncryptionType EncryptionType)
		{
			mstrOriginalString = OriginalString;
			mbytEncryptionType = EncryptionType;

			return this.Encrypt();
		}

		#endregion

		#region " Metodos de Desencriptacion "

		public string Decrypt()
		{
			ICryptoTransform ct;
			MemoryStream ms;
			CryptoStream cs;
			byte[] byt;

			ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);

			byt = Convert.FromBase64String(mstrEncryptedString);

			ms = new MemoryStream();
			cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
			cs.Write(byt, 0, byt.Length);
			cs.FlushFinalBlock();
			cs.Close();

			mstrOriginalString = Encoding.UTF8.GetString(ms.ToArray());

			return mstrOriginalString;
		}

		public string Decrypt(string EncryptedString)
		{
			mstrEncryptedString = EncryptedString;
           // return mstrEncryptedString;
			return this.Decrypt();
		}

		public string Decrypt(string EncryptedString, PDSAEncryptionType EncryptionType)
		{
			mstrEncryptedString = EncryptedString;
			mbytEncryptionType = EncryptionType;

			return this.Decrypt();
		}

		#endregion

		#region " Metodo SetEncryptor() "

		private void SetEncryptor()
		{
			switch(mbytEncryptionType)
			{
				case PDSAEncryptionType.DES:
					mCSP = new DESCryptoServiceProvider();
					break;
				case PDSAEncryptionType.RC2:
					mCSP = new RC2CryptoServiceProvider();
					break;
				case PDSAEncryptionType.Rijndael:
					mCSP = new RijndaelManaged();
					break;
				case PDSAEncryptionType.TripleDES:
					mCSP = new TripleDESCryptoServiceProvider();
					break;
			}
      
			// Generate Key
			this.GenerateKey();

			// Generate IV
			this.GenerateIV();
		}
		#endregion

		#region " Metodos Publicos Varios "

		public byte[] GenerateKey()
		{
			try
			{
				int x = 0;
				char[] chars = ConfigurationSettings.AppSettings["encKey"].ToCharArray();
				byte[] bits = new byte[chars.Length];
				foreach (char c in chars)
				{
					bits[x] = Convert.ToByte(c);
					x += 1;
				}
										  
				mCSP.Key = bits;

				return mCSP.Key;
			}
			catch (Exception ex)
			{
				if (ex.GetType().Name.Equals("ArgumentException"))
					throw new Exception("La clave encKey debe tener 8 digitos"); 
				else
					throw new Exception("Clave encKey en Archivo de Configuracion no implementada"); 
			}
		}

		public byte[] GenerateIV()
		{
			try
			{
				int x = 0;
				char[] chars = ConfigurationSettings.AppSettings["encKey"].ToCharArray();
				byte[] bits = new byte[chars.Length];
				foreach (char c in chars)
				{
					bits[x] = Convert.ToByte(c);
					x += 1;
				}
										  
				mCSP.IV = bits;

				return mCSP.IV;
			}
			catch (Exception ex)
			{
				if (ex.GetType().Name.Equals("ArgumentException"))
					throw new Exception("La clave encKey debe tener 8 digitos"); 
				else
					throw new Exception("Clave encKey en Archivo de Configuracion no implementada"); 
			}
		}

		#endregion

        #region " Metodos Carga Componentes "


      

        public void CargarCombo(DropDownList Combo, String strSql, String CampoId, String CampoDetalle)
        {
            NHibernate.Cfg.Configuration oConf = new NHibernate.Cfg.Configuration();
            String strconn = oConf.GetProperty("hibernate.connection.connection_string");
            SqlDataAdapter da = new SqlDataAdapter(strSql, strconn);
            DataSet ds = new DataSet();
            da.Fill(ds, "t");
            Combo.DataTextField = CampoDetalle;
            Combo.DataValueField = CampoId;
            Combo.DataSource = ds.Tables["t"];
            Combo.DataBind();
            da.Dispose();
            ds.Dispose();
         
        }

        public void CargarCombo(DropDownList Combo, IList lista, String CampoId, String CampoDetalle)
        {
            Combo.DataTextField = CampoDetalle;
            Combo.DataValueField = CampoId;
            Combo.DataSource = lista;
            Combo.DataBind();           
        }

        public void CargarCheckBox(CheckBoxList Checks, String strSql, String CampoId, String CampoDetalle)
        {
            NHibernate.Cfg.Configuration oConf = new NHibernate.Cfg.Configuration();
            String strconn = oConf.GetProperty("hibernate.connection.connection_string");
            SqlDataAdapter da = new SqlDataAdapter(strSql, strconn);
            DataSet ds = new DataSet();
            da.Fill(ds, "t");
            Checks.DataTextField = CampoDetalle;
            Checks.DataValueField = CampoId;
            Checks.DataSource = ds.Tables["t"];
            Checks.DataBind();

        }

        public void CargarListBox(ListBox Lista, String strSql, String CampoId, String CampoDetalle)
        {
            NHibernate.Cfg.Configuration oConf = new NHibernate.Cfg.Configuration();
            String strconn = oConf.GetProperty("hibernate.connection.connection_string");
            SqlDataAdapter da = new SqlDataAdapter(strSql, strconn);
            DataSet ds = new DataSet();
            da.Fill(ds, "t");
            Lista.DataTextField = CampoDetalle;
            Lista.DataValueField = CampoId;
            Lista.DataSource = ds.Tables["t"];
            Lista.DataBind();

        }


        #endregion

        public SqlDataReader getDataReader(String strSql)
        {
            NHibernate.Cfg.Configuration oConf = new NHibernate.Cfg.Configuration();
            String strConn = oConf.GetProperty("hibernate.connection.connection_string");
            SqlConnection myConnection = new SqlConnection(strConn);
            SqlCommand myCommand = new SqlCommand(strSql, myConnection);
            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader();
            return dr;
        }
           

        public DataTable getDataSet(String strSql, bool conColu)
        {       
            NHibernate.Cfg.Configuration oConf = new NHibernate.Cfg.Configuration();
            String strconn = oConf.GetProperty("hibernate.connection.connection_string");
            SqlDataAdapter da2 = new SqlDataAdapter(strSql, strconn);
            DataSet ds2 = new DataSet();            
            da2.Fill(ds2, "t");
            if (conColu == true)
            {
                DataColumn vModifica = new DataColumn();
                vModifica.DefaultValue = "";
                DataColumn vElimina = new DataColumn();
                vElimina.DefaultValue = "";
                ds2.Tables["t"].Columns.Add(vModifica);
                ds2.Tables["t"].Columns.Add(vElimina);
            }
            return ds2.Tables[0];
        }
        #region " Otros M�todos "

        public bool EsNumerico(string val)
        {
            bool match;
            //regula expression to match numeric values
            string pattern = "(^[-+]?\\d+(,?\\d*)*\\.?\\d*([Ee][-+]\\d*)?$)|(^[-+]?\\d?(,?\\d*)*\\.\\d+([Ee][-+]\\d*)?$)";
            //generate new Regulsr Exoression eith the pattern and a couple RegExOptions
            Regex regEx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            //tereny expresson to see if we have a match or not
            match = regEx.Match(val).Success ? true : false;
            //return the match value (true or false)
            return match;
        }
        public bool EsEntero(string val)
        {
            bool match;
            //regula expression to match numeric values
            string pattern = "(^\\d*$)";
            //generate new Regulsr Exoression eith the pattern and a couple RegExOptions
            Regex regEx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            //tereny expresson to see if we have a match or not
            match = regEx.Match(val).Success ? true : false;
            //return the match value (true or false)
            return match;
        }   

        public string SacaComillas(string cadena)
        {
            string Caracter;
            string Comillas = "''";
            string Blanco = "";
            string cadenaLimpia = cadena.Replace(Comillas, Blanco);
            Comillas = "'";
            Blanco = "";
            cadenaLimpia = cadenaLimpia.Replace(Comillas, Blanco);
            Caracter = "*";
            Blanco = "";
            cadenaLimpia = cadenaLimpia.Replace(Caracter, Blanco);
            Caracter = "/";
            Blanco = "";
            cadenaLimpia = cadenaLimpia.Replace(Caracter, Blanco);
            Caracter = "\\";
            Blanco = "";
            cadenaLimpia = cadenaLimpia.Replace(Caracter, Blanco);
            Caracter = ":";
            Blanco = "";
            cadenaLimpia = cadenaLimpia.Replace(Caracter, Blanco);
            Caracter = "?";
            Blanco = "";
            cadenaLimpia = cadenaLimpia.Replace(Caracter, Blanco);
            Caracter = "<";
            Blanco = "";
            cadenaLimpia = cadenaLimpia.Replace(Caracter, Blanco);
            Caracter = ">";
            Blanco = "";
            cadenaLimpia = cadenaLimpia.Replace(Caracter, Blanco);
            Caracter = "|";
            Blanco = "";
            cadenaLimpia = cadenaLimpia.Replace(Caracter, Blanco);

            cadenaLimpia = RemoverSignosAcentos(cadenaLimpia);

            return cadenaLimpia;
        }

        public int VerificaPermisos(ArrayList lista, string m_Objeto)
        {
            int per = 0;          
            foreach (string item in lista)
            {
                string[] arr = item.Split((":").ToCharArray());
                if (arr[0] == m_Objeto)
                {
                    per = int.Parse(arr[1]);
                    break;
                }
            }
            return per;
        }


        /// con �
        //private const string ConSignos = "��������������u������������������";
        //private const string SinSignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUcC";

        /// sin �
        private const string ConSignos = "��������������u����������������Ǳ";
        private const string SinSignos = "aaaeeeiiiooouuuAAAEEEIIIOOOUUUcCn";



        public  string RemoverSignosAcentos(string texto)
        {
            var textoSinAcentos = string.Empty;

            foreach (var caracter in texto)
            {
                var indexConAcento = ConSignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos = textoSinAcentos + (SinSignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos = textoSinAcentos + (caracter);
            }
            return textoSinAcentos;
        }


        public bool bisiesto(int anno)
  {
      bool resultado;
      //Comprobamos la regla general.
      //Si anno es divisible por 4, es decir, si el
      //resto de la divisi�n entre 4 es 0...
      if (anno % 4 == 0)
      {
          //Si es divisible por 4, ahora toca comprobar
          //la excepci�n
          if (
               (anno % 100 == 0) &&  //Si es divisible por 100
               (anno % 400 != 0)     //y no por 400
             )
          {
              resultado = false; //entonces no es bisiesto
          }
          else
          {
              resultado = true; //No cumple la excepci�n.
              //Lo dejamos como bisiesto por ser divisible por 4
          }
      }
      else //Si no cumple la regla general
      {
          //No es bisiesto.
          resultado = false;
      }
      return resultado;
} 
        public string Formato(string p)
        {
            string aux = "";
            switch (p)
            {
                case "0": aux = "0"; break;
                case "1": aux = "0.0"; break;
                case "2": aux = "0.00"; break;
                case "3": aux = "0.000"; break;
                case "4": aux = "0.0000"; break;
            }
            return aux;
        }


        public string DiferenciaFechas(DateTime dn, DateTime fechaProtocolo)
        { ///calculo de fechas teniendo el cuenta los dias de los meses            

            DateTime da = fechaProtocolo; // DateTime.Now;
            int  anos =  da.Year - dn.Year; // calculamos a�os 
            int meses = da.Month - dn.Month; // calculamos meses 
            int dias =  da.Day - dn.Day; // calculamos d�as 

            //ajuste de posible negativo en $d�as 
            if (dias < 0) 
            { 
                //--$meses; 
                int dias_mes_anterior=0;
                //ahora hay que sumar a $dias los dias que tiene el mes anterior de la fecha actual 
                switch (da.Month) { 
                case 1:     dias_mes_anterior=31; break; 
                case 2:     dias_mes_anterior=31; break; 
                case 3:  
                    if (bisiesto(da.Year )) 
                    { 
                        dias_mes_anterior=29; break; 
                    } else { 
                        dias_mes_anterior=28; break; 
                    } 
                case 4:     dias_mes_anterior=31; break; 
                case 5:     dias_mes_anterior=30; break; 
                case 6:     dias_mes_anterior=31; break; 
                case 7:     dias_mes_anterior=30; break; 
                case 8:     dias_mes_anterior=31; break; 
                case 9:     dias_mes_anterior=31; break; 
                case 10:    dias_mes_anterior=30; break; 
                case 11:    dias_mes_anterior=31; break; 
                case 12:    dias_mes_anterior=30; break; 
                } 
                dias=dias + dias_mes_anterior;
                meses = meses - 1;
            }
           
             
            //ajuste de posible negativo en $meses 
            if (meses < 0)
            {   //--$anos; 
                meses = meses + 12;
            }
            
            string edad = "1;D";
            if (anos > 0)
            {
                if ((da.Month < dn.Month)&&(anos==1))
                {
                    if (meses > 0) edad = meses.ToString() + ";M";
                    else
                        if (dias > 0) edad = dias.ToString() + ";D";
                }
                else
                {
                    if (da.Month < dn.Month) anos=anos-1;
                    edad = anos.ToString() + ";A";
                }
            }
            else
            {
                if (meses> 0) edad = meses.ToString() + ";M";
                else
                    if (dias > 0) edad = dias.ToString() + ";D";
            }
            return edad;
        }

        



        #endregion
    }
}