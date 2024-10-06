import React from "react";
import { Box, Grid, TextField, Button, Typography, Paper } from "@mui/material";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { userServices } from "../../service/userService";
import { useDispatch, useSelector } from "react-redux";
import {
  showPromiseToast,
  updatePromiseToast,
} from "../../helpers/toastHelpers";
import { useNavigate } from "react-router-dom";

const schema = z.object({
  username: z.string().min(1, "Username is required"),
  password: z.string().min(6, "Password must be at least 6 characters long"),
});

const Login = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(schema),
  });
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const onSubmit = async (data) => {
   
    const toastId = showPromiseToast("Logging in Please wait....");

    try {
      const response = await userServices.login(data, dispatch);
      if (response) {
        updatePromiseToast(toastId, "Welcome", "success");
        navigate("/user");
      } else {
        updatePromiseToast(toastId, "Login Failed Try Again", "error");
      }
    } catch (err) {
      updatePromiseToast(toastId, "Login Error", "error");
    }
  };

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
        backgroundColor: "#ffe8cc",
        position: "relative",
      }}
    >
      <Paper
        elevation={3}
        sx={{
          width: "80%",
          height: "80vh",
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <Grid container component="main">
          <Grid
            item
            xs={false}
            sm={4}
            md={6}
            sx={{
              backgroundImage: `url('/loginBg.jpg')`,
              backgroundRepeat: "no-repeat",
              backgroundColor: "#f5f5f5",
              backgroundSize: "cover",
              backgroundPosition: "center",
              display: { xs: "none", md: "block" },
              flexGrow: 1,
            }}
          />

          <Grid
            item
            xs={12}
            sm={12}
            md={6}
            component={Box}
            display="flex"
            flexDirection="column"
            justifyContent="center"
            alignItems="center"
            sx={{ flexGrow: 1 }}
          >
            <Box sx={{ maxWidth: 400, p: 4, flexGrow: 1 }}>
              <Typography variant="h4" component="h1" gutterBottom>
                Log In
              </Typography>

              {/* Input Fields */}
              <form onSubmit={handleSubmit(onSubmit)}>
                <TextField
                  {...register("username")}
                  label="User Name"
                  fullWidth
                  margin="normal"
                  error={!!errors.username}
                  helperText={errors.username?.message}
                  InputProps={{
                    startAdornment: (
                      <i
                        className="fas fa-user"
                        style={{ marginRight: "10px" }}
                      />
                    ),
                  }}
                />
                <TextField
                  {...register("password")}
                  label="Password"
                  type="password"
                  fullWidth
                  margin="normal"
                  error={!!errors.password}
                  helperText={errors.password?.message}
                  InputProps={{
                    startAdornment: (
                      <i
                        className="fas fa-lock"
                        style={{ marginRight: "10px" }}
                      />
                    ),
                  }}
                />

                <Button
                  type="submit"
                  fullWidth
                  variant="contained"
                  color="primary"
                  sx={{ mt: 2, mb: 2 }}
                >
                  Log In
                </Button>
              </form>
            </Box>
          </Grid>
        </Grid>
      </Paper>

     
      <Box
        sx={{
          position: "absolute",
          bottom: 20,
          right: 20,
          backgroundColor: "#ffeb3b",
          padding: 2,
          borderRadius: 2,
          boxShadow: 3,
        }}
      >
        <Typography variant="body2">
       Test :
        <br />   
          ID: Admin
          <br />
          Password: Admin@123
        </Typography>
      </Box>
    </Box>
  );
};

export default Login;
