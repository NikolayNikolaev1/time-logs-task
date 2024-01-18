import {
  Box,
  FormControl,
  FormControlLabel,
  FormLabel,
  Radio,
  RadioGroup,
} from "@mui/material";
import TimeLogsTable from "../TimeLogsTable";
import BarChart from "../BarChart";
import useContent from "./useContent";
import { ChartResourceType } from "./utils";
import useApplicationContext from "../../context/ApplicationContext";

const Content = () => {
  const { comparedChartBar } = useApplicationContext();
  const {
    topUsers,
    topProjects,
    selectedResource,
    handleSelectedResourceOnChange,
  } = useContent();

  return (
    <div style={{ display: "flex" }}>
      <Box sx={{ width: "50%" }}>
        <TimeLogsTable />
      </Box>
      <BarChart
        resourceLabel={`Top 10 ${selectedResource} by Hours worked`}
        metricLabel="Hours"
        //@ts-ignore
        // TODO: filter out undefined data from compared without using ts-ignore.
        data={
          selectedResource === ChartResourceType.Users
            ? [
                ...topUsers.map((u) => [
                  `${u.firstName} ${u.lastName}`,
                  u.hoursWorked,
                ]),
                [comparedChartBar?.label, comparedChartBar?.value],
              ].filter((v) => !v.includes(undefined))
            : topProjects.map((p) => [p.name, p.hoursWorked])
        }
        comparedData={
          selectedResource === ChartResourceType.Users &&
          comparedChartBar !== null
            ? { label: "Compared", value: comparedChartBar.value }
            : undefined
        }
      />
      <FormControl>
        <FormLabel>Select resource for chart</FormLabel>
        <RadioGroup
          row
          name="position"
          defaultValue="top"
          value={selectedResource}
          onChange={handleSelectedResourceOnChange}
        >
          <FormControlLabel
            value="users"
            control={<Radio />}
            label="Users"
            labelPlacement="top"
          />
          <FormControlLabel
            value="projects"
            control={<Radio />}
            label="Projects"
            labelPlacement="top"
          />
        </RadioGroup>
      </FormControl>
    </div>
  );
};

export default Content;
