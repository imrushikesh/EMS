import axios from "axios";

export const apiInstance = (options) => {
  const customConfiguration = {
    method: options.method,
    baseURL: options.url,
    headers: options.headers,
    data: options.data,
    responseType: options.responseType ?? "json",
  };
  return axios(customConfiguration);
};
