import {
  Box,
  FormControl,
  FormControlLabel,
  FormLabel,
  Radio,
  RadioGroup,
} from "@mui/material";
import TimeLogsTable from "../TimeLogsTable";
import CompareChart from "../CompareChart";
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

      <FormControl sx={{ width: "100%" }}>
        <div style={{ marginLeft: "15%" }}>
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
        </div>
        <CompareChart
          title={`Top ${selectedResource} by Hours worked`}
          resourceLabel={selectedResource}
          metricLabel="Hours"
          //@ts-ignore
          // TODO: filter out undefined data from compared without using ts-ignore.
          data={
            selectedResource === ChartResourceType.Users
              ? [
                  ...topUsers.map((u) => [u.email, u.hoursWorked]),
                  [comparedChartBar?.label, comparedChartBar?.value],
                ]
                  .filter((v) => !v.includes(undefined))
                  .filter(
                    (value, index, self) =>
                      index === self.findIndex((t) => t[0] === value[0]),
                  )
              : topProjects.map((p) => [p.name, p.hoursWorked])
          }
          comparedData={
            selectedResource === ChartResourceType.Users &&
            comparedChartBar !== null
              ? { label: "Compared", value: comparedChartBar.value }
              : undefined
          }
        />
      </FormControl>
    </div>
  );
};

export default Content;
