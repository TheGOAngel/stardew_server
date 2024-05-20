// Code generated by protoc-gen-go-grpc. DO NOT EDIT.
// versions:
// - protoc-gen-go-grpc v1.2.0
// - protoc             (unknown)
// source: junimohost/customers/v1/customers.proto

package customersv1

import (
	context "context"
	grpc "google.golang.org/grpc"
	codes "google.golang.org/grpc/codes"
	status "google.golang.org/grpc/status"
)

// This is a compile-time assertion to ensure that this generated file
// is compatible with the grpc package it is being compiled against.
// Requires gRPC-Go v1.32.0 or later.
const _ = grpc.SupportPackageIsVersion7

// CustomersServiceClient is the client API for CustomersService service.
//
// For semantics around ctx use and closing/ending streaming RPCs, please refer to https://pkg.go.dev/google.golang.org/grpc/?tab=doc#ClientConn.NewStream.
type CustomersServiceClient interface {
	GetMe(ctx context.Context, in *GetMeRequest, opts ...grpc.CallOption) (*GetMeResponse, error)
}

type customersServiceClient struct {
	cc grpc.ClientConnInterface
}

func NewCustomersServiceClient(cc grpc.ClientConnInterface) CustomersServiceClient {
	return &customersServiceClient{cc}
}

func (c *customersServiceClient) GetMe(ctx context.Context, in *GetMeRequest, opts ...grpc.CallOption) (*GetMeResponse, error) {
	out := new(GetMeResponse)
	err := c.cc.Invoke(ctx, "/junimohost.customers.v1.CustomersService/GetMe", in, out, opts...)
	if err != nil {
		return nil, err
	}
	return out, nil
}

// CustomersServiceServer is the server API for CustomersService service.
// All implementations must embed UnimplementedCustomersServiceServer
// for forward compatibility
type CustomersServiceServer interface {
	GetMe(context.Context, *GetMeRequest) (*GetMeResponse, error)
	mustEmbedUnimplementedCustomersServiceServer()
}

// UnimplementedCustomersServiceServer must be embedded to have forward compatible implementations.
type UnimplementedCustomersServiceServer struct {
}

func (UnimplementedCustomersServiceServer) GetMe(context.Context, *GetMeRequest) (*GetMeResponse, error) {
	return nil, status.Errorf(codes.Unimplemented, "method GetMe not implemented")
}
func (UnimplementedCustomersServiceServer) mustEmbedUnimplementedCustomersServiceServer() {}

// UnsafeCustomersServiceServer may be embedded to opt out of forward compatibility for this service.
// Use of this interface is not recommended, as added methods to CustomersServiceServer will
// result in compilation errors.
type UnsafeCustomersServiceServer interface {
	mustEmbedUnimplementedCustomersServiceServer()
}

func RegisterCustomersServiceServer(s grpc.ServiceRegistrar, srv CustomersServiceServer) {
	s.RegisterService(&CustomersService_ServiceDesc, srv)
}

func _CustomersService_GetMe_Handler(srv interface{}, ctx context.Context, dec func(interface{}) error, interceptor grpc.UnaryServerInterceptor) (interface{}, error) {
	in := new(GetMeRequest)
	if err := dec(in); err != nil {
		return nil, err
	}
	if interceptor == nil {
		return srv.(CustomersServiceServer).GetMe(ctx, in)
	}
	info := &grpc.UnaryServerInfo{
		Server:     srv,
		FullMethod: "/junimohost.customers.v1.CustomersService/GetMe",
	}
	handler := func(ctx context.Context, req interface{}) (interface{}, error) {
		return srv.(CustomersServiceServer).GetMe(ctx, req.(*GetMeRequest))
	}
	return interceptor(ctx, in, info, handler)
}

// CustomersService_ServiceDesc is the grpc.ServiceDesc for CustomersService service.
// It's only intended for direct use with grpc.RegisterService,
// and not to be introspected or modified (even as a copy)
var CustomersService_ServiceDesc = grpc.ServiceDesc{
	ServiceName: "junimohost.customers.v1.CustomersService",
	HandlerType: (*CustomersServiceServer)(nil),
	Methods: []grpc.MethodDesc{
		{
			MethodName: "GetMe",
			Handler:    _CustomersService_GetMe_Handler,
		},
	},
	Streams:  []grpc.StreamDesc{},
	Metadata: "junimohost/customers/v1/customers.proto",
}