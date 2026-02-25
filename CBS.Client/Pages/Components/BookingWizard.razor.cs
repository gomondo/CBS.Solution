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
        private FormData1 _formData1 = new FormData1();
        private FormData2 _formData2 = new FormData2();
        private FinishFormData _finishFormData = new FinishFormData();
        private bool _overlayIsVisible = false;

        void OnStepChange(FluentWizardStepChangeEventArgs e)
        {

        }

        async Task OnFinishedAsync()
        {

        }

        async Task OnValidSubmit()
        {

        }

        void OnInvalidSubmit()
        {

        }

        private class FormData1
        {
            [Required]
            [MaxLength(3)]
            public string? FirstName { get; set; }

            [Required]
            [MinLength(10)]
            public string? LastName { get; set; }
        }

        private class FormData2
        {
            [Required]
            public string? AddressLine1 { get; set; }

            public string? AddressLine2 { get; set; }

            [Required]
            public string? City { get; set; }

            [Required]
            public string? StateOrProvince { get; set; }

            [Required]
            public string? Country { get; set; }

            [Required]
            public string? PostalCode { get; set; }
        }

        private class FinishFormData
        {
            [Required]
            [MinLength(5)]
            public string? Signature { get; set; }
        }
    }
}
