import { endpoints } from "../constants/APIEndpoints";
import { apiService } from "./apiService";

async function getAllEmployee() {
  return apiService.GET(endpoints.GET_ALL_EMP);
}

async function deleteEmployeeById(id) {
  return apiService.DELETE(endpoints.DELETE_EMP + `${id}`);
}

async function getEmployeeById(id) {
  return apiService.GET(endpoints.GET_EMP_BY_ID + `${id}`);
}

async function createEmployee(data) {
  return apiService.POST(endpoints.CREATE_EMP, data);
}

async function updateEmployee(data) {
  return apiService.PUT(endpoints.UPDATE_EMP, data);
}

export const employeeService = {
  getAllEmployee,
  getEmployeeById,
  deleteEmployeeById,
  createEmployee,
  updateEmployee,
};
