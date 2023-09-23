using System.Text;

namespace ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models
{
    /// <summary>
    /// Represents a Respnse for a remote control command.
    /// </summary>
    public class RemoteResponse
    {
        /// <summary>
        /// Trus if the command was acknowledged and executed.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error messge to return to the user.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Returns the current instance with the given error message and the Success property set to false.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public RemoteResponse WithError(string error) 
        { 
            this.ErrorMessage = error;
            this.Success = false;
            return this;
        }

        /// <summary>
        /// Returns the current instance with the Success property set to true.
        /// </summary>        
        /// <returns></returns>
        public RemoteResponse AsSuccess()
        {            
            this.Success = true;
            return this;
        }
    }
}
