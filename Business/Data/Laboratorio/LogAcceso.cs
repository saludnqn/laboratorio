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
	public sealed class LogAcceso: Business.BaseDataAccess
	{

		#region Private Members
		private bool m_isChanged;

		private int m_idlogacceso; 
		private int m_idusuario; 
		private DateTime m_fecha; 		
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public LogAcceso()
		{
			m_idlogacceso = 0; 
			m_idusuario = 0; 
			m_fecha = DateTime.MinValue; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public LogAcceso(
			int idusuario, 
			DateTime fecha)
			: this()
		{
			m_idusuario = idusuario;
			m_fecha = fecha;
		}
		#endregion // End Required Fields Only Constructor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public int IdLogAcceso
		{
			get { return m_idlogacceso; }
			set
			{
				m_isChanged |= ( m_idlogacceso != value ); 
				m_idlogacceso = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public int IdUsuario
		{
			get { return m_idusuario; }
			set
			{
				m_isChanged |= ( m_idusuario != value ); 
				m_idusuario = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public DateTime Fecha
		{
			get { return m_fecha; }
			set
			{
				m_isChanged |= ( m_fecha != value ); 
				m_fecha = value;
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
