import { Route, Routes } from "react-router-dom";
import Login from "../domain/authentication/Login";
import User from "../domain/user/User";
import Employee from "../domain/employee/Employee";
function Routing() {
  return (
    <Routes>
      <Route path="*" element={<Login />} />
      <Route path="/" element={<Login />} />
      <Route path="/login" element={<Login />} />
      <Route path="/user" element={<User />} />
      <Route path="/employee" element={<Employee />} />
    </Routes>
  );
}

export default Routing;
