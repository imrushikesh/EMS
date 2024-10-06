import React, { useState, useEffect } from "react";
import { DataGrid } from "@mui/x-data-grid";
import {
  Button,
  Drawer,
  MenuItem,
  Select,
  TextField,
  Box,
  FormControl,
  InputLabel,
  FormHelperText,
  Paper,
  AppBar,
  Typography,
  Toolbar,
} from "@mui/material";
import { useForm, Controller } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  showPromiseToast,
  updatePromiseToast,
} from "../../helpers/toastHelpers";
import { employeeService } from "../../service/employeeService";
import { userServices } from "../../service/userService";
import { useNavigate } from "react-router-dom";

const schema = z.object({
  empName: z.string().min(1, "Name must not be blank"),
  empEmail: z.string().min(1, "email must not be blank"),
});

const Employee = () => {
  const [allEmp, setAllEmp] = useState([]);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [selectedEmp, setSelectedEmp] = useState(null);

  const {
    register,
    handleSubmit,
    reset,
    setValue,
    control,
    clearErrors,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(schema),
  });

  const navigate=useNavigate();
  useEffect(() => {
    fetchEmp();
  }, []);

  const fetchEmp = () => {
    employeeService.getAllEmployee().then((response) => {
      if (response.isSuccess) {
        setAllEmp(response.result);
      }
    });
  };

  const openDrawer = (emp = null) => {
    clearErrors();
    setSelectedEmp(emp);
    if (emp) {
      setValue("empName", emp.empName);
      setValue("empEmail", emp.empEmail);
    } else {
      reset({
        empName: "",
        empEmail: [],
      });
    }
    setDrawerOpen(true);
  };

  const closeDrawer = () => {
    setDrawerOpen(false);
    setSelectedEmp(null);
  };

  const onSubmit = async (data) => {
    if (selectedEmp) {
      const payload = {
        empId: selectedEmp.empId,
        empName: data.empName,
        empEmail: data.empEmail,
      };
      const toastId = showPromiseToast("updating Employee");
      employeeService.updateEmployee(payload).then((Response) => {
        if (Response.isSuccess) {
          updatePromiseToast(toastId, "employee Updated", "success");
          fetchEmp();
          closeDrawer();
        } else {
          updatePromiseToast(toastId, Response.message, "error");
          closeDrawer();
        }
      });
    } else {
      const payload = {
        empName: data.empName,
        empEmail: data.empEmail,
      };
      const toastId = showPromiseToast("Adding Employee");
      employeeService.createEmployee(payload).then((Response) => {
        if (Response.isSuccess) {
          updatePromiseToast(toastId, "Employee Created", "success");
          fetchEmp();
          closeDrawer();
        } else {
          updatePromiseToast(toastId, Response.message, "error");
        }
      });
    }
    closeDrawer();
  };

  const deleteEmp = (data) => {
    const toastId = showPromiseToast("deleting Employee");
    employeeService.deleteEmployeeById(data.empId).then((Response) => {
      if (Response.isSuccess) {
        updatePromiseToast(toastId, "Employee deleted", "success");
        fetchEmp();
        closeDrawer();
      } else {
        updatePromiseToast(toastId, Response.message, "error");
        closeDrawer();
      }
    });
  };

  const columns = [
    { field: "empId", headerName: "ID", width: 200 },
    { field: "empName", headerName: "Name", width: 300 },
    { field: "empEmail", headerName: "Email", width: 300 },
    {
      field: "edit",
      headerName: "Action",
      sortable: false,
      width: 200,
      renderCell: (params) => (
        <>
          <Button
            onClick={() => openDrawer(params.row)}
            variant="outlined"
            sx={{ marginRight: "2px" }}
          >
            Edit
          </Button>
          <Button
            onClick={() => {
              deleteEmp(params.row);
            }}
            variant="outlined"
          >
            Delete
          </Button>
        </>
      ),
    },
  ];

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        backgroundColor: "#f5f5f5",
      }}
    >
      <AppBar position="static" sx={{ marginBottom: "5px" }}>
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Employee
          </Typography>
          <Button color="inherit" onClick={()=>{userServices.logout(); navigate("/login")}}>logOut</Button>
        </Toolbar>
      </AppBar>
      <Paper
        elevation={3}
        sx={{
          width: "80%",
          display: "flex",
          flexDirection: "column",
          justifyContent: "center",
          alignItems: "flex-end",
        }}
      >
        <Button
          onClick={() => openDrawer()}
          variant="contained"
          style={{ margin: "10px" }}
        >
          Add Employee
        </Button>
        <Box style={{ height: 600, width: "100%" }}>
          <DataGrid
            rows={allEmp}
            columns={columns}
            getRowId={(row) => row.empId}
          />
        </Box>

        <Drawer anchor="right" open={drawerOpen} onClose={closeDrawer}>
          <AppBar position="static" sx={{ marginBottom: "5px" }}>
            <Toolbar>
              <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                {selectedEmp ? "Update Employee" : "Add Employee"}
              </Typography>
            </Toolbar>
          </AppBar>
          <Box sx={{ width: 300, padding: 2 }}>
            <form onSubmit={handleSubmit(onSubmit)}>
              <TextField
                label="Employee Name"
                fullWidth
                margin="normal"
                {...register("empName")}
                error={!!errors.empName}
                helperText={errors.empName?.message}
              />

              <TextField
                label="Email"
                fullWidth
                margin="normal"
                {...register("empEmail")}
                error={!!errors.empEmail}
                helperText={errors.empEmail?.message}
              />

              <Button type="submit" variant="contained" color="primary">
                {selectedEmp ? "Update" : "Add"}
              </Button>
              <Button
                onClick={closeDrawer}
                variant="outlined"
                color="secondary"
                style={{ marginLeft: "10px" }}
              >
                Cancel
              </Button>
            </form>
          </Box>
        </Drawer>
      </Paper>
    </Box>
  );
};

export default Employee;
