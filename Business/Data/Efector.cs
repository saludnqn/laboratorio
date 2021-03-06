/*
insert license info here
*/
using System;
using System.Collections;

namespace Business.Data
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
    public sealed class Efector : Business.BaseDataAccess
	{

		#region Private Members
		private bool m_isChanged;

		private int m_idefector; 
		private string m_nombre; 
		private Zona m_idzona; 
		private string m_nombrenacion; 
		private string m_complejidad; 
		private int m_idefectorsuperior; 
		private string m_domicilio; 
		private string m_telefono; 
		private string m_reponsable; 		
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public Efector()
		{
			m_idefector = 0; 
			m_nombre = String.Empty; 
			m_idzona =new Zona(); 
			m_nombrenacion = String.Empty; 
			m_complejidad = String.Empty; 
			m_idefectorsuperior =0; 
			m_domicilio = String.Empty; 
			m_telefono = String.Empty; 
			m_reponsable = String.Empty; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public Efector(
			string nombre, 
			Zona idzona, 
			string nombrenacion, 
			string complejidad, 
			int idefectorsuperior, 
			string domicilio, 
			string telefono, 
			string reponsable)
			: this()
		{
			m_nombre = nombre;
			m_idzona = idzona;
			m_nombrenacion = nombrenacion;
			m_complejidad = complejidad;
			m_idefectorsuperior = idefectorsuperior;
			m_domicilio = domicilio;
			m_telefono = telefono;
			m_reponsable = reponsable;
		}
		#endregion // End Required Fields Only Constructor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public int IdEfector
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
				
				if(  value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Nombre", value, value.ToString());
				
				m_isChanged |= (m_nombre != value); m_nombre = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>
		public Zona IdZona
		{
			get { return m_idzona; }
			set
			{
				m_isChanged |= ( m_idzona != value ); 
				m_idzona = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public string NombreNacion
		{
			get { return m_nombrenacion; }

			set	
			{	
				if( value == null )					throw new ArgumentOutOfRangeException("Null value not allowed for NombreNacion", value, "null");
				
				if(  value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for NombreNacion", value, value.ToString());
				
				m_isChanged |= (m_nombrenacion != value); m_nombrenacion = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>
		public string Complejidad
		{
			get { return m_complejidad; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for Complejidad", value, "null");
				
				if(  value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Complejidad", value, value.ToString());
				
				m_isChanged |= (m_complejidad != value); m_complejidad = value;
			}
		}
        /// <summary>
        /// 
        /// </summary>
        public int IdEfectorSuperior
        {
            get { return m_idefectorsuperior; }
            set
            {
                m_isChanged |= (m_idefectorsuperior != value);
                m_idefectorsuperior = value;
            }

        }
		/// <summary>
		/// 
		/// </summary>
		public string Domicilio
		{
			get { return m_domicilio; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for Domicilio", value, "null");
				
				if(  value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Domicilio", value, value.ToString());
				
				m_isChanged |= (m_domicilio != value); m_domicilio = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>
		public string Telefono
		{
			get { return m_telefono; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for Telefono", value, "null");
				
				if(  value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Telefono", value, value.ToString());
				
				m_isChanged |= (m_telefono != value); m_telefono = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>
		public string Reponsable
		{
			get { return m_reponsable; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for Reponsable", value, "null");
				
				if(  value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Reponsable", value, value.ToString());
				
				m_isChanged |= (m_reponsable != value); m_reponsable = value;
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
