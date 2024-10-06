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

import { userServices } from "../../service/userService";
import {
  showPromiseToast,
  updatePromiseToast,
} from "../../helpers/toastHelpers";
import { useNavigate } from "react-router-dom";

const schema = z.object({
  userName: z.string().min(1, "Username must not be blank"),
  role: z.array(z.string()).min(1, "At least one role must be selected"),
});

const rolesMap = {
  1: "Admin",
  2: "Manager",
  3: "Employee",
};

const User = () => {
  const navigate=useNavigate();
  const [allUsers, setAllUsers] = useState([]);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [selectedUser, setSelectedUser] = useState(null);

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

  useEffect(() => {
    fetchUser();
  }, []);

  const fetchUser = () => {
    userServices.getAllUsers().then((response) => {
      if (response.isSuccess) {
        setAllUsers(response.result);
      }
    });
  };

  const openDrawer = (user = null) => {
    clearErrors();
    setSelectedUser(user);
    if (user) {
      setValue("role", user.role.split(","));
      setValue("userName", user.userName);
    } else {
      reset({
        userName: "",
        role: [],
      });
    }
    setDrawerOpen(true);
  };

  const closeDrawer = () => {
    setDrawerOpen(false);
    setSelectedUser(null);
  };

  const onSubmit = async (data) => {
    if (selectedUser) {
      const payload = {
        userId: selectedUser.userId,
        userName: data.userName,
        role: data.role.join(","),
      };
      const toastId = showPromiseToast("updating User");
      userServices.updateUser(payload).then((Response) => {
        if (Response.isSuccess) {
          updatePromiseToast(toastId, "User Updated", "success");
          fetchUser();
          closeDrawer();
        } else {
          updatePromiseToast(toastId, Response.message, "error");
          closeDrawer();
        }
      });
    } else {
      const payload = {
        userName: data.userName,
        role: data.role.join(","),
      };
      const toastId = showPromiseToast("Adding User");
      userServices.createUser(payload).then((Response) => {
        if (Response.isSuccess) {
          updatePromiseToast(toastId, "User Created", "success");
          fetchUser();
          closeDrawer();
        } else {
          updatePromiseToast(toastId, Response.message, "error");
        }
      });
    }
    closeDrawer();
  };

  const deleteUser = (data) => {
    const toastId = showPromiseToast("deleting User");
    userServices.deleteUserById(data.userId).then((Response) => {
      if (Response.isSuccess) {
        updatePromiseToast(toastId, "User deleted", "success");
        fetchUser();
        closeDrawer();
      } else {
        updatePromiseToast(toastId, Response.message, "error");
        closeDrawer();
      }
    });
  };

  const columns = [
    { field: "userId", headerName: "User ID", width: 200 },
    { field: "userName", headerName: "User Name", width: 300 },
    {
      field: "role",
      headerName: "Role",
      width: 500,
      valueGetter: (params) =>
        params.row.role
          .split(",")
          .map((role) => rolesMap[role])
          .join(", "),
    },
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
            disabled={params.row.userName=="Admin" ? true:false}
          >
            Edit
          </Button>
          <Button
            onClick={() => {
              deleteUser(params.row);
            }}
            variant="outlined"
            disabled={params.row.userName=="Admin" ? true:false}

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
            User
          </Typography>
          <Button color="inherit" onClick={()=>{navigate("/employee")}}>Employee</Button>
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
          Add User
        </Button>
        <Box style={{ height: 600, width: "100%" }}>
          <DataGrid
            rows={allUsers}
            columns={columns}
            getRowId={(row) => row.userId}
          />
        </Box>

        <Drawer anchor="right" open={drawerOpen} onClose={closeDrawer}>
          <AppBar position="static" sx={{ marginBottom: "5px" }}>
            <Toolbar>
              <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                {selectedUser ? "Update User" : "Add user"}
              </Typography>
            </Toolbar>
          </AppBar>
          <Box sx={{ width: 300, padding: 2 }}>
            <form onSubmit={handleSubmit(onSubmit)}>
              <TextField
                label="User Name"
                fullWidth
                margin="normal"
                {...register("userName")}
                error={!!errors.userName}
                helperText={errors.userName?.message}
              />

              {/* Role Selection with Controller */}
              <FormControl fullWidth margin="normal" error={!!errors.role}>
                <InputLabel>Role</InputLabel>
                <Controller
                  name="role"
                  control={control}
                  defaultValue={[]}
                  render={({ field }) => (
                    <Select
                      label="Role"
                      multiple
                      value={field.value}
                      onChange={field.onChange}
                    >
                      <MenuItem value="1">Admin</MenuItem>
                      <MenuItem value="2">Manager</MenuItem>
                      <MenuItem value="3">Employee</MenuItem>
                    </Select>
                  )}
                />
                {errors.role && (
                  <FormHelperText>{errors.role.message}</FormHelperText>
                )}
              </FormControl>

              <Button type="submit" variant="contained" color="primary">
                {selectedUser ? "Update" : "Add"}
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

export default User;
