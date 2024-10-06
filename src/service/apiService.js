import { apiInstance } from "./apiInstance";
import { userServices } from "./userService";

const createRequest = async (methodType, endPoint, data = null) => {
  const requestOptions = {
    method: methodType,
    url: `${process.env.REACT_APP_BACKEND_URL}${endPoint}`,
    headers: {
      "Content-Type": "application/json, application/text, */*",
      Authorization: `Bearer ${userServices.getJwtToken() ?? ""}`,
    },
    data: data,
  };

  try {
    const response = await apiInstance(requestOptions);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
};

export const apiService = {
  GET: (endPoint) => createRequest(httpVerbs.GET, endPoint),
  POST: (endPoint, data = null) =>
    createRequest(httpVerbs.POST, endPoint, data),
  PUT: (endPoint, data = null) => createRequest(httpVerbs.PUT, endPoint, data),
  DELETE: (endPoint, data = null) =>
    createRequest(httpVerbs.DELETE, endPoint, data),
};
const httpVerbs = {
  GET: "GET",
  POST: "POST",
  PUT: "PUT",
  DELETE: "DELETE",
};
