import { combineReducers } from "redux";
import userSlice from "./User/UserSlice";

const rootReducer = combineReducers({
  user: userSlice,
});

export default rootReducer;
