

using System;
using System.Collections;

namespace Business.Data.Laboratorio
{
	/// <summary>
	///	Generated by MyGeneration using the NHibernate Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class MensajeInterno: Business.BaseDataAccess
	{

		#region Private Members
		private bool m_isChanged;

		private int m_idmensaje; 
		private string m_remitente; 
		private string m_destinatario; 
		private string m_mensaje; 
		private int m_idusuarioregistro; 
		private DateTime m_fechahoraregistro; 		
		#endregion

		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public MensajeInterno()
		{
			m_idmensaje = 0; 
			m_remitente = String.Empty; 
			m_destinatario = String.Empty; 
			m_mensaje = String.Empty; 
			m_idusuarioregistro = 0; 
			m_fechahoraregistro = DateTime.MinValue; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor

		#region Required Fields Only Constructor
		/// <summary>
		/// required (not null) fields only constructor
		/// </summary>
		public MensajeInterno(
			string remitente, 
			string destinatario, 
			string mensaje, 
			int idusuarioregistro, 
			DateTime fechahoraregistro)
			: this()
		{
			m_remitente = remitente;
			m_destinatario = destinatario;
			m_mensaje = mensaje;
			m_idusuarioregistro = idusuarioregistro;
			m_fechahoraregistro = fechahoraregistro;
		}
		#endregion // End Required Fields Only Constructor

		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>
		public int IdMensaje
		{
			get { return m_idmensaje; }
			set
			{
				m_isChanged |= ( m_idmensaje != value ); 
				m_idmensaje = value;
			}

		}
			
		/// <summary>
		/// 
		/// </summary>
		public string Remitente
		{
			get { return m_remitente; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for Remitente", value, "null");
				
				if(  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Remitente", value, value.ToString());
				
				m_isChanged |= (m_remitente != value); m_remitente = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>
		public string Destinatario
		{
			get { return m_destinatario; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for Destinatario", value, "null");
				
				if(  value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Destinatario", value, value.ToString());
				
				m_isChanged |= (m_destinatario != value); m_destinatario = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>
		public string Mensaje
		{
			get { return m_mensaje; }

			set	
			{	
				if( value == null )
					throw new ArgumentOutOfRangeException("Null value not allowed for Mensaje", value, "null");
				
				if(  value.Length > 4000)
					throw new ArgumentOutOfRangeException("Invalid value for Mensaje", value, value.ToString());
				
				m_isChanged |= (m_mensaje != value); m_mensaje = value;
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
		public DateTime FechaHoraRegistro
		{
			get { return m_fechahoraregistro; }
			set
			{
				m_isChanged |= ( m_fechahoraregistro != value ); 
				m_fechahoraregistro = value;
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