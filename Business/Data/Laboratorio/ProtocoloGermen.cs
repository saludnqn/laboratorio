/*
insert license info here
*/
using System;
using System.Collections;

namespace Business.Data.Laboratorio
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class ProtocoloGermen: Business.BaseDataAccess
	{

		#region Private Members
		private bool m_isChanged;

		private int m_idprotocologermen; 
		private Protocolo m_idprotocolo;
        private int m_numeroaislamiento;
        private int m_iditem;
		private Germen m_idgermen; 
		private bool m_atb; 
		private string m_observaciones; 
		private bool m_baja; 
		private int m_idusuarioregistro; 
		private DateTime m_fecharegistro;
        private int m_idusuariovalida;
        private DateTime m_fechavalida;
        #endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public ProtocoloGermen()
		{
			m_idprotocologermen = 0; 
			m_idprotocolo = new Protocolo(); 
			m_idgermen = new Germen();
            m_numeroaislamiento = 0;
            m_iditem = 0;
			m_atb = false; 
			m_observaciones = String.Empty; 
			m_baja = false; 
			m_idusuarioregistro = 0; 
			m_fecharegistro = DateTime.MinValue;
            m_idusuariovalida = 0;
            m_fechavalida = DateTime.MinValue;
        }
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public ProtocoloGermen(
			Protocolo idprotocolo, 
			Germen idgermen, 
            int numeroaislamiento,
			bool atb, 
			string observaciones, 
			bool baja, 
			int idusuarioregistro, 
			DateTime fecharegistro,
                int idusuariovalida,
            DateTime fechavalida)
			: this()
		{
			m_idprotocolo = idprotocolo;
			m_idgermen = idgermen;
            m_numeroaislamiento = numeroaislamiento;
			m_atb = atb;
			m_observaciones = observaciones;
			m_baja = baja;
			m_idusuarioregistro = idusuarioregistro;
			m_fecharegistro = fecharegistro;
            m_idusuariovalida = idusuariovalida;
            m_fechavalida = fechavalida;
        }

		#endregion // End Required Fields Only Constructor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public int IdProtocoloGermen
		{
			get { return m_idprotocologermen; }
			set
			{
				m_isChanged |= ( m_idprotocologermen != value ); 
				m_idprotocologermen = value;
			}

		}

        public int IdItem
        {
            get { return m_iditem; }
            set
            {
                m_isChanged |= (m_iditem != value);
                m_iditem = value;
            }

        }

        public int NumeroAislamiento
        {
            get { return m_numeroaislamiento; }
            set
            {
                m_isChanged |= (m_numeroaislamiento != value);
                m_numeroaislamiento = value;
            }

        }
		/// <summary>
		/// 
		/// </summary>
		public Protocolo IdProtocolo
		{
			get { return m_idprotocolo; }
			set
			{
				m_isChanged |= ( m_idprotocolo != value ); 
				m_idprotocolo = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public Germen IdGermen
		{
			get { return m_idgermen; }
			set
			{
				m_isChanged |= ( m_idgermen != value ); 
				m_idgermen = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public bool Atb
		{
			get { return m_atb; }
			set
			{
				m_isChanged |= ( m_atb != value ); 
				m_atb = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public string Observaciones
		{
			get { return m_observaciones; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for Observaciones", value, "null");
				
				if(  value.Length > 4000)
					throw new ArgumentOutOfRangeException("Invalid value for Observaciones", value, value.ToString());
				
				m_isChanged |= (m_observaciones != value); m_observaciones = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>
		public bool Baja
		{
			get { return m_baja; }
			set
			{
				m_isChanged |= ( m_baja != value ); 
				m_baja = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public int IdUsuarioRegistro
		{
			get { return m_idusuarioregistro; }
			set
			{
				m_isChanged |= ( m_idusuarioregistro != value ); 
				m_idusuarioregistro = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public DateTime FechaRegistro
		{
			get { return m_fecharegistro; }
			set
			{
				m_isChanged |= ( m_fecharegistro != value ); 
				m_fecharegistro = value;
			}

		}


        /// <summary>
        /// 
        /// </summary>
        public int IdUsuarioValida
        {
            get { return m_idusuariovalida; }
            set
            {
                m_isChanged |= (m_idusuariovalida != value);
                m_idusuariovalida = value;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FechaValida
        {
            get { return m_fechavalida; }
            set
            {
                m_isChanged |= (m_fechavalida != value);
                m_fechavalida = value;
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
	
        //public void GrabarMecanismoResistencia(int p)
        //{
        //    MecanismoResistencia oMecanismo = new MecanismoResistencia();
        //    oMecanismo = (MecanismoResistencia)oMecanismo.Get(typeof(MecanismoResistencia), p);

        //    ProtocoloMecanismoResistencia oRegistro = new ProtocoloMecanismoResistencia();
        //    oRegistro.IdProtocoloGermen = this;
        //    oRegistro.IdMecanismoResistencia = oMecanismo;
        //    oRegistro.Save();
        //}
    }
}
