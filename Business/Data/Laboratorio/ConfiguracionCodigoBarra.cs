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
	public sealed class ConfiguracionCodigoBarra: Business.BaseDataAccess
	{

		#region Private Members
		private bool m_isChanged;

		private int m_idconfiguracioncodigobarra; 
		private TipoServicio m_idtiposervicio;
        private bool m_habilitado; 
		private string m_fuente; 
		private bool m_protocolofecha; 
		private bool m_protocoloorigen; 
		private bool m_protocolosector; 
		private bool m_protocolonumeroorigen;
        private bool m_pacientenumerodocumento; 
		private bool m_pacienteapellido; 
		private bool m_pacientesexo; 
		private bool m_pacienteedad; 		
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public ConfiguracionCodigoBarra()
		{
			m_idconfiguracioncodigobarra = 0; 
			m_idtiposervicio =new TipoServicio(); 
			m_fuente = String.Empty;
            m_habilitado = false;

			m_protocolofecha = false; 
			m_protocoloorigen = false; 
			m_protocolosector = false; 
			m_protocolonumeroorigen = false;
            m_pacientenumerodocumento = false;
			m_pacienteapellido = false; 
			m_pacientesexo = false; 
			m_pacienteedad = false; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public ConfiguracionCodigoBarra(
			TipoServicio idtiposervicio, 
            bool habilitado,
			string fuente, 
			bool protocolofecha, 
			bool protocoloorigen, 
			bool protocolosector, 
			bool protocolonumeroorigen, 
            bool pacientenumerodocumento,
			bool pacienteapellido, 
			bool pacientesexo, 
			bool pacienteedad)
			: this()
		{
			m_idtiposervicio = idtiposervicio;
            m_habilitado = habilitado;
			m_fuente = fuente;
			m_protocolofecha = protocolofecha;
			m_protocoloorigen = protocoloorigen;
			m_protocolosector = protocolosector;
			m_protocolonumeroorigen = protocolonumeroorigen;
            pacientenumerodocumento = m_pacientenumerodocumento;
			m_pacienteapellido = pacienteapellido;
			m_pacientesexo = pacientesexo;
			m_pacienteedad = pacienteedad;
		}
		#endregion // End Required Fields Only Constructor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public int IdConfiguracionCodigoBarra
		{
			get { return m_idconfiguracioncodigobarra; }
			set
			{
				m_isChanged |= ( m_idconfiguracioncodigobarra != value ); 
				m_idconfiguracioncodigobarra = value;
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
		public string Fuente
		{
			get { return m_fuente; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for Fuente", value, "null");
				
				if(  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Fuente", value, value.ToString());
				
				m_isChanged |= (m_fuente != value); m_fuente = value;
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
		/// 
		/// </summary>
		public bool ProtocoloFecha
		{
			get { return m_protocolofecha; }
			set
			{
				m_isChanged |= ( m_protocolofecha != value ); 
				m_protocolofecha = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public bool ProtocoloOrigen
		{
			get { return m_protocoloorigen; }
			set
			{
				m_isChanged |= ( m_protocoloorigen != value ); 
				m_protocoloorigen = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public bool ProtocoloSector
		{
			get { return m_protocolosector; }
			set
			{
				m_isChanged |= ( m_protocolosector != value ); 
				m_protocolosector = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public bool ProtocoloNumeroOrigen
		{
			get { return m_protocolonumeroorigen; }
			set
			{
				m_isChanged |= ( m_protocolonumeroorigen != value ); 
				m_protocolonumeroorigen = value;
			}

		}

        /// <summary>
        /// 
        /// </summary>
        public bool PacienteNumeroDocumento
        {
            get { return m_pacientenumerodocumento; }
            set
            {
                m_isChanged |= (m_pacientenumerodocumento != value);
                m_pacientenumerodocumento = value;
            }

        }
			
		/// <summary>
		/// 
		/// </summary>
		public bool PacienteApellido
		{
			get { return m_pacienteapellido; }
			set
			{
				m_isChanged |= ( m_pacienteapellido != value ); 
				m_pacienteapellido = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public bool PacienteSexo
		{
			get { return m_pacientesexo; }
			set
			{
				m_isChanged |= ( m_pacientesexo != value ); 
				m_pacientesexo = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public bool PacienteEdad
		{
			get { return m_pacienteedad; }
			set
			{
				m_isChanged |= ( m_pacienteedad != value ); 
				m_pacienteedad = value;
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