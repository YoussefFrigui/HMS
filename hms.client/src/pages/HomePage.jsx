import React from 'react';
import { Button, AppBar, Toolbar, Typography } from '@mui/material';
import { Link } from 'react-router-dom';
import { apiClient } from '@/services/apiClient'; 
import '../assets/styles/index.css';

const HomePage = () => {
  return (
    <div>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" style={{ flexGrow: 1 }}>
            Hospital Management System
          </Typography>
          <Button color="inherit" component={Link} to="/login">
            Login
          </Button>
          <Button color="inherit" component={Link} to="/register">
            Register
          </Button>
        </Toolbar>
      </AppBar>
      <div style={{ padding: '20px' }}>
        <Typography variant="h4" gutterBottom>
          Welcome to the Hospital Management System
        </Typography>
        <Typography variant="body1">
          Please login or register to continue.
        </Typography>
      </div>
    </div>
  );
};

export default HomePage;