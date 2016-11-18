namespace WebApi.ErrorHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net;


    public interface IApiExceptions
    {
        int ErrorCode { get; set; }

        string ErrorDescription { get; set; }

        HttpStatusCode HttpStatus { get; set; }

        string ReasonPhrase { get; set; }
    }
}
