import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  loading: false,
  user: {},
  error: null,
};

const userSlice = createSlice({
  name: "User",
  initialState,
  reducers: {
    loginRequest: (state) => {
      state.loading = true;
    },
    loginRequestSuccess: (state, action) => {
      state.loading = false;
      state.user = action.payload;
      state.error = null;
    },
    loginRequestFailure: (state, action) => {
      state.loading = false;
      state.user = {};
      state.error = action.payload;
    },
  },
});

export const { loginRequest, loginRequestSuccess, loginRequestFailure } =
  userSlice.actions;

export default userSlice.reducer;
