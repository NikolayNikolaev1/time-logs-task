import { Box } from "@mui/material";
import TimeLogsTable from "./components/TimeLogsTable";
import { FilterProvider } from "./context/FilterContext";
import Header from "./components/Header/Header";

const App = () => {
  return (
    <FilterProvider>
      <Header />
      <Box sx={{ width: "50%" }}>
        <TimeLogsTable />
      </Box>
    </FilterProvider>
  );
};

export default App;
