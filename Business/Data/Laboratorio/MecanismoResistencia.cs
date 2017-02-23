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
    public sealed class MecanismoResistencia : Business.BaseDataAccess
	{

		#region Private Members
		private bool m_isChanged;

		private int m_idmecanismoresistencia; 		
		private string m_nombre; 
		private bool m_baja; 
		
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public MecanismoResistencia()
		{
			m_idmecanismoresistencia = 0; 
		
			m_nombre = String.Empty; 
			m_baja = false; 
			
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public MecanismoResistencia(
			
			string nombre, 
			bool baja
			)
			: this()
		{
			
			m_nombre = nombre;
			m_baja = baja;
			
		}
		#endregion // End Required Fields Only Constructor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public int IdMecanismoResistencia
		{
			get { return m_idmecanismoresistencia; }
			set
			{
                m_isChanged |= (m_idmecanismoresistencia != value);
                m_idmecanismoresistencia = value;
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
				
				if(  value.Length >1050)
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
