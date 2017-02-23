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
	public sealed class ProtocoloIncidenciaCalidad: Business.BaseDataAccess
	{

		#region Private Members
		private bool m_isChanged;

		private int m_idprotocoloincidenciacalidad; 
		private int m_idprotocolo; 
		private int m_idefector; 
		private int m_idincidenciacalidad; 		
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public ProtocoloIncidenciaCalidad()
		{
			m_idprotocoloincidenciacalidad = 0; 
			m_idprotocolo = 0; 
			m_idefector = 0; 
			m_idincidenciacalidad = 0; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public ProtocoloIncidenciaCalidad(
			int idprotocolo, 
			int idefector, 
			int idincidenciacalidad)
			: this()
		{
			m_idprotocolo = idprotocolo;
			m_idefector = idefector;
			m_idincidenciacalidad = idincidenciacalidad;
		}
		#endregion // End Required Fields Only Constructor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public int IdProtocoloIncidenciaCalidad
		{
			get { return m_idprotocoloincidenciacalidad; }
			set
			{
				m_isChanged |= ( m_idprotocoloincidenciacalidad != value ); 
				m_idprotocoloincidenciacalidad = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public int IdProtocolo
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
		public int IdIncidenciaCalidad
		{
			get { return m_idincidenciacalidad; }
			set
			{
				m_isChanged |= ( m_idincidenciacalidad != value ); 
				m_idincidenciacalidad = value;
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
