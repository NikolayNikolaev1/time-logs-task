import { Box } from "@mui/material";
import "./App.css";
import TimeLogsTable from "./components/TimeLogsTable";

const App = () => {
  return (
    <Box sx={{ width: "50%" }}>
      <TimeLogsTable />
    </Box>
  );
};

export default App;
