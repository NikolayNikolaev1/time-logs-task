import { Stack } from "@mui/material";
import DateRangePicker from "../DateRangePicker";
import ResetButton from "../ResetButton";
import useFilterContext from "../../context/FilterContext";
import { DateRange } from "moment-range";

const Header = () => {
  const { handleDateRangeChange } = useFilterContext();

  return (
    <Stack direction="row">
      <DateRangePicker
        onChange={(dateRange: DateRange | null) => {
          if (dateRange === null) {
            handleDateRangeChange(null);
            return;
          }

          handleDateRangeChange({
            startDate: dateRange.start.toISOString(),
            endDate: dateRange.end.toISOString(),
          });
        }}
      />
      <ResetButton />
    </Stack>
  );
};

export default Header;
