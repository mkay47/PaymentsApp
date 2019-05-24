using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace NotificationServiceAPI.Extensions
{
    public class CustomSMSHeaderBehaviour : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            return;
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(new CustomHeaderInspector());
            dispatchOperation.ParameterInspectors.Add(new CustomSMSInspector());
        }

        public void Validate(OperationDescription operationDescription)
        {
            return;
        }
    }
}