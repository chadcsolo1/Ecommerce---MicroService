using Grpc.Core;
using Google.Rpc;
using GoogleStatus = Google.Rpc.Status;
using GrpcStatus = Grpc.Core.Status;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
//using Microsoft.AspNetCore.Http.HttpResults;

namespace Discount.Extensions
{
    public static class GrpcErrorHelper
    {
        public static RpcException CreateValidationException(Dictionary<string, string> fieldErrors)
        {
            var fieldViolations = new List<BadRequest.Types.FieldViolation>();

            foreach (var error in fieldErrors)
            {
                fieldViolations.Add(new BadRequest.Types.FieldViolation
                {
                    Field = error.Key,
                    Description = error.Value
                });
            }

            //Now Add Bad Request
            var badRequest = new BadRequest();
            badRequest.FieldViolations.AddRange(fieldViolations);

            var status = new GoogleStatus {
                Code = (int)StatusCode.InvalidArgument,
                Message = "Validation Failed",
                Details = {Any.Pack(badRequest) }
            };

            var trailers = new Metadata()
            {
                { "grpc-status-details-bin", status.ToByteArray() }
            };

            return new RpcException(new GrpcStatus(StatusCode.InvalidArgument, "Validation Failed"), trailers);
        }
        //public static RpcException Internal(string message)
        //{
        //    return new RpcException(new Status(StatusCode.Internal, message));
        //}
        //public static RpcException InvalidArgument(string message)
        //{
        //    return new RpcException(new Status(StatusCode.InvalidArgument, message));
        //}
    }
}
