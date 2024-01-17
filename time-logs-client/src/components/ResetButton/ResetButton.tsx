import { Box, Button, CircularProgress } from "@mui/material";
import useResetButton from "./useResetButton";

const ResetButton = () => {
  const { isLoading, handleResetOnClick } = useResetButton();

  return (
    <Box sx={{ display: "flex" }}>
      <Button variant="outlined" onClick={handleResetOnClick}>
        Reset
      </Button>
      {isLoading && <CircularProgress />}
    </Box>
  );
};

export default ResetButton;
