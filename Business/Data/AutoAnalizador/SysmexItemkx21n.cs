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
	public sealed class SysmexItemkx21n: Business.BaseDataAccess
	{

		#region Private Members
		private bool m_isChanged;

		private int m_idsysmexitem; 
		private string m_idsysmex; 
		private int m_iditem; 
		private bool m_habilitado; 		
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public SysmexItemkx21n()
		{
			m_idsysmexitem = 0; 
			m_idsysmex = String.Empty; 
			m_iditem = 0; 
			m_habilitado = false; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public SysmexItemkx21n(
			string idsysmex, 
			int iditem, 
			bool habilitado)
			: this()
		{
			m_idsysmex = idsysmex;
			m_iditem = iditem;
			m_habilitado = habilitado;
		}
		#endregion // End Required Fields Only Constructor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public int IdSysmexItem
		{
			get { return m_idsysmexitem; }
			set
			{
				m_isChanged |= ( m_idsysmexitem != value ); 
				m_idsysmexitem = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public string IdSysmex
		{
			get { return m_idsysmex; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for IdSysmex", value, "null");
				
				if(  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for IdSysmex", value, value.ToString());
				
				m_isChanged |= (m_idsysmex != value); m_idsysmex = value;
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
		public bool Habilitado
		{
			get { return m_habilitado; }
			set
			{
				m_isChanged |= ( m_habilitado != value ); 
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
