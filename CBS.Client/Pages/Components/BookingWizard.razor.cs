using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;


namespace CBS.Client.Pages.Components
{
    public partial class BookingWizard : ComponentBase
    {
        public BookingWizard()
        {

        }
        private bool _overlayIsVisible = false;

        [Inject]
        private NavigationManager NavigationManager { get; set; }   
        protected void GoToBooking()
        {
            NavigationManager.NavigateTo("bookings");
        }


    }
}
