import { applicationConstants } from "../constants/ApplicationConstants";
import { jwtDecode } from "jwt-decode";
import {
  loginRequest,
  loginRequestFailure,
  loginRequestSuccess,
} from "../stateManagement/User/UserSlice";
import { endpoints } from "../constants/APIEndpoints";
import { apiService } from "./apiService";
const login = async (loginDetails, dispatch) => {
  dispatch(loginRequest());
  try {
    const resp = await apiService.POST(endpoints.LOGIN, loginDetails);
    if (resp.isSuccess) {
      saveJwtToken(resp.result);
      const user = jwtDecode(resp.result);
      dispatch(loginRequestSuccess(user));
      return true;
    } else {
      dispatch(loginRequestFailure(resp.message));
      return false;
    }
  } catch (err) {
    dispatch(loginRequestFailure(err));
    return false;
  }
};

const saveJwtToken = (token) => {
  sessionStorage.setItem(applicationConstants.JWTSessionKey, token);
};
const getJwtToken = () => {
  return sessionStorage.getItem(applicationConstants.JWTSessionKey);
};

const logout = () => {
  sessionStorage.removeItem(applicationConstants.JWTSessionKey);
};

async function getAllUsers() {
  return apiService.GET(endpoints.GET_ALL_USERS);
}

async function deleteUserById(id) {
  return apiService.DELETE(endpoints.DELETE_USER + `${id}`);
}

async function getUserById(id) {
  return apiService.GET(endpoints.GET_USER_BY_ID + `${id}`);
}

async function createUser(data) {
  return apiService.POST(endpoints.CREATE_USER, data);
}

async function updateUser(data) {
  return apiService.PUT(endpoints.UPDATE_USER, data);
}

export const userServices = {
  login,
  getJwtToken,
  getAllUsers,
  getUserById,
  createUser,
  deleteUserById,
  updateUser,
  logout,
};
