import { toast } from "react-toastify";

export const showPromiseToast = (message) => {
  const toastId = toast.loading(message);
  return toastId;
};

export const updatePromiseToast = (toastId, message, type) => {
  toast.update(toastId, {
    render: message,
    type: type,
    isLoading: false,
    autoClose: 3000,
  });
};

export const showErrorToast = (message) => {
  toast.error(message);
};

export const showWarningToast = (message) => {
  toast.warning(message);
};