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
    public sealed class Area : Business.BaseDataAccess
	{

		#region Private Members
		private bool m_isChanged;

		private int m_idarea; 
		private Efector m_idefector; 
		private string m_nombre; 
		private TipoServicio m_idtiposervicio; 
		//private Usuario m_idusuarioresponsable; 
		private bool m_baja;
        private int m_ordenimpresion;
        private bool m_imprimecodigobarra;
		private Usuario m_idusuarioregistro; 
		private DateTime m_fecharegistro; 		
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Area()
		{
			m_idarea = 0; 
			m_idefector = new Efector(); 
			m_nombre = String.Empty; 
			m_idtiposervicio = new TipoServicio(); 
		//	m_idusuarioresponsable = new Usuario(); 
			m_baja = false;
            m_imprimecodigobarra = false;
            m_ordenimpresion = 0;
			m_idusuarioregistro = new Usuario(); 
			m_fecharegistro = DateTime.MinValue; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public Area(
			Efector idefector, 
			string nombre, 
			TipoServicio idtiposervicio, 
		//	Usuario idusuarioresponsable, 
            bool imprimecodigobarra,
			bool baja, 
            int ordenimpresion,
			Usuario idusuarioregistro, 
			DateTime fecharegistro)
			: this()
		{
			m_idefector = idefector;
			m_nombre = nombre;
			m_idtiposervicio = idtiposervicio;
			//m_idusuarioresponsable = idusuarioresponsable;
            m_imprimecodigobarra = imprimecodigobarra;
			m_baja = baja;
            m_ordenimpresion = ordenimpresion;
			m_idusuarioregistro = idusuarioregistro;
			m_fecharegistro = fecharegistro;
		}
		#endregion // End Required Fields Only Constructor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public int IdArea
		{
			get { return m_idarea; }
			set
			{
				m_isChanged |= ( m_idarea != value ); 
				m_idarea = value;
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
		public TipoServicio IdTipoServicio
		{
			get { return m_idtiposervicio; }
			set
			{
				m_isChanged |= ( m_idtiposervicio != value ); 
				m_idtiposervicio = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
        public int OrdenImpresion
        {
            get { return m_ordenimpresion; }
            set
            {
                m_isChanged |= (m_ordenimpresion != value);
                m_ordenimpresion = value;
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
        public bool ImprimeCodigoBarra
        {
            get { return m_imprimecodigobarra; }
            set
            {
                m_isChanged |= (m_imprimecodigobarra != value);
                m_imprimecodigobarra = value;
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
