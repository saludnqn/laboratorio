/*
insert license info here
*/
using System;
using System.Collections;

namespace Business.Data.AutoAnalizador
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class MindrayItem: Business.BaseDataAccess
	{

		#region Private Members
		private bool m_isChanged;

		private int m_idmindrayitem; 
		private int m_idmindray; 
		private int m_iditem; 
		private string m_tipomuestra;
        private string m_prefijo;
        private bool m_habilitado; 	

		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public MindrayItem()
		{
			m_idmindrayitem = 0; 
			m_idmindray = 0; 
			m_iditem = 0; 
			m_tipomuestra = String.Empty;
            m_prefijo = String.Empty;
            m_habilitado = true; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public MindrayItem(
			int idmindray, 
			int iditem, 
			string tipomuestra,
            string prefijo,
            bool habilitado)
			: this()
		{
			m_idmindray = idmindray;
			m_iditem = iditem;
			m_tipomuestra = tipomuestra;
            m_prefijo = prefijo;
            m_habilitado = habilitado;
		}
		#endregion // End Required Fields Only Constructor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public int IdMindrayItem
		{
			get { return m_idmindrayitem; }
			set
			{
				m_isChanged |= ( m_idmindrayitem != value ); 
				m_idmindrayitem = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public int IdMindray
		{
			get { return m_idmindray; }
			set
			{
				m_isChanged |= ( m_idmindray != value ); 
				m_idmindray = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public int IdItem
		{
			get { return m_iditem; }
			set
			{
				m_isChanged |= ( m_iditem != value ); 
				m_iditem = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public string TipoMuestra
		{
			get { return m_tipomuestra; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for TipoMuestra", value, "null");
				
				if(  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TipoMuestra", value, value.ToString());
				
				m_isChanged |= (m_tipomuestra != value); m_tipomuestra = value;
			}
        }
        public string Prefijo
        {
            get { return m_prefijo; }

            set
            {
                if (value == null)
                    throw new ArgumentOutOfRangeException("Null value not allowed for m_prefijo", value, "null");

                if (value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for m_prefijo", value, value.ToString());

                m_isChanged |= (m_prefijo != value); m_prefijo = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Habilitado
        {
            get { return m_habilitado; }
            set
            {
                m_isChanged |= (m_habilitado != value);
                m_habilitado = value;
            }

        }
		/// <summary>
		/// Returns whether or not the object has changed it's values.
		/// </summary>
		public bool IsChanged
		{
			get { return m_isChanged; }
		}
				
		#endregion 
	}
}
