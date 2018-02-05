using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using NetMidApi.Models;

namespace NetMidApi.Common.ApiPack
{
    public  class CommonController: ApiController
    {

        #region webapi v2

        public IHttpActionResult OkEx<T>(T result)
        {
            
            return Ok(new ApiResult<T>
            {
                Success = true,
                Result = result,
                Error = null
            });
        }
        public IHttpActionResult CreatedEx()
        {
            return CreatedEx<string>();
        }
        public IHttpActionResult NoContentEx()
        {
            return NoContentEx<string>();
        }
        public IHttpActionResult NotFoundEx()
        {
            return NotFoundEx<string>();
        }
        public IHttpActionResult ErrorEx( string errorMessage)
        {
            return ErrorEx<string>(errorMessage);
        }
        public IHttpActionResult BadRequestEx( string message)
        {
            return BadRequestEx<string>(message);
        }

        #endregion

        #region Simple<T>

        public IHttpActionResult CreatedEx<T>()
        {
            return Content(HttpStatusCode.Created
                , new ApiResult<T>
                {
                    Success = true,
                    Result = default(T),
                    Error = null
                });
        }
        public IHttpActionResult NoContentEx<T>()
        {
            return Content(HttpStatusCode.NoContent
                , new ApiResult<T>
                {
                    Success = true,
                    Result = default(T),
                    Error = null
                });
        }
        public IHttpActionResult NotFoundEx<T>()
        {
            return Content(HttpStatusCode.NotFound
                , new ApiResult<T>
                {
                    Success = true,
                    Result = default(T),
                    Error = null
                });
        }
        
        public IHttpActionResult BadRequestEx<T>(string message)
        {
            return Content(HttpStatusCode.BadRequest,new ApiResult <T>
            {
                Success = false,
                Result = default(T),
                Error = new Dictionary<string, object>
                {
                    { "code" , 10012 },
                    { "message" , "A required parameter is missing or doesn't have the right format:" + message}
                    , { "details",null}
                    , {"validationErrors",null }
                }
            });
        }
        public IHttpActionResult ErrorEx<T>(string errorMessage)
        {
            return Content(HttpStatusCode.InternalServerError
                , new ApiResult<T>
                {
                    Success = false,
                    Result = default(T),
                    Error = new Dictionary<string, object>
                    {
                        { "code" , 10003 },
                        { "message" , errorMessage}
                        , { "details",null}
                        , {"validationErrors",null }
                    }

                });
        }

        #endregion

        #region Stay

        public IHttpActionResult BadRequestEx<T>(Dictionary<string, object> dicError)
        {
            return Content(HttpStatusCode.BadRequest,new ApiResult<T>
            {
                Success = false,
                Result = default(T),
                Error = dicError
            });
        }
        public IHttpActionResult ErrorEx<T>( Dictionary<string, object> dicError)
        {
            return Content(HttpStatusCode.InternalServerError
                , new ApiResult<T>
                {
                    Success = false,
                    Result = default(T),
                    Error = dicError
                });
        }

        #endregion

    }
}
