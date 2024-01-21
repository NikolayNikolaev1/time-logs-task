import { Box, Button } from "@mui/material";
import useResetButton from "./useResetButton";

const ResetButton = () => {
  const { handleResetOnClick } = useResetButton();

  return (
    <Box>
      <Button variant="outlined" onClick={handleResetOnClick}>
        Reset
      </Button>
    </Box>
  );
};

export default ResetButton;
