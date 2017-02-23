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
	public sealed class Rutina: Business.BaseDataAccess
	{

		#region Private Members
		private bool m_isChanged;

		private int m_idrutina; 
		private Efector m_idefector;
        private TipoServicio m_idtiposervicio; 
		private string m_nombre; 
		private bool m_baja; 
		private Usuario m_idusuarioregistro; 
		private DateTime m_fecharegistro;
        private bool m_peticionelectronica;
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Rutina()
		{
			m_idrutina = 0; 
			m_idefector = new Efector();
            m_idtiposervicio = new TipoServicio(); 
			m_nombre = String.Empty; 
			m_baja = false; 
			m_idusuarioregistro = new Usuario(); 
			m_fecharegistro = DateTime.MinValue;
            m_peticionelectronica = false;
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public Rutina(
			Efector idefector,
            TipoServicio idtiposervicio, 
			string nombre, 
			bool baja, 
			Usuario idusuarioregistro, 
			DateTime fecharegistro,
            bool peticionelectronica)
			: this()
		{
			m_idefector = idefector;
            m_idtiposervicio = idtiposervicio;
			m_nombre = nombre;
			m_baja = baja;
			m_idusuarioregistro = idusuarioregistro;
			m_fecharegistro = fecharegistro;
            m_peticionelectronica = peticionelectronica;
		}
		#endregion // End Required Fields Only Constructor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public int IdRutina
		{
			get { return m_idrutina; }
			set
			{
				m_isChanged |= ( m_idrutina != value ); 
				m_idrutina = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public Efector IdEfector
		{
			get { return m_idefector; }
			set
			{
				m_isChanged |= ( m_idefector != value ); 
				m_idefector = value;
			}

		}


        public TipoServicio IdTipoServicio
        {
            get { return m_idtiposervicio; }
            set
            {
                m_isChanged |= (m_idtiposervicio != value);
                m_idtiposervicio = value;
            }

        }
			
		/// <summary>
		/// 
		/// </summary>
		public string Nombre
		{
			get { return m_nombre; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for Nombre", value, "null");
				
				if(  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Nombre", value, value.ToString());
				
				m_isChanged |= (m_nombre != value); m_nombre = value;
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
        public bool PeticionElectronica
        {
            get { return m_peticionelectronica; }
            set
            {
                m_isChanged |= (m_peticionelectronica != value);
                m_peticionelectronica = value;
            }

        }
		/// <summary>
		/// 
		/// </summary>
		public Usuario IdUsuarioRegistro
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
		/// Returns whether or not the object has changed it's values.
		/// </summary>
		public bool IsChanged
		{
			get { return m_isChanged; }
		}
				
		#endregion 
	}
}
