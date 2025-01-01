import React from 'react';
import { Box, AppBar, Toolbar, Typography, Button, Drawer, List, ListItem, ListItemIcon, ListItemText } from '@mui/material';
import { useAuth } from '../../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import DashboardIcon from '@mui/icons-material/Dashboard';
import EventIcon from '@mui/icons-material/Event';
import MessageIcon from '@mui/icons-material/Message';
import PeopleIcon from '@mui/icons-material/People';
import DescriptionIcon from '@mui/icons-material/Description';

const Layout = ({ children }) => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const getNavItems = () => {
    switch (user?.role) {
      case 'Patient':
        return [
          { text: 'Dashboard', icon: <DashboardIcon />, path: '/patient/dashboard' },
          { text: 'Medical History', icon: <DescriptionIcon />, path: '/patient/medical-history' },
          { text: 'Messages', icon: <MessageIcon />, path: '/messages' },
        ];
      case 'Doctor':
        return [
          { text: 'Dashboard', icon: <DashboardIcon />, path: '/doctor/dashboard' },
          { text: 'Appointments', icon: <EventIcon />, path: '/appointments' },
          { text: 'Patient Records', icon: <DescriptionIcon />, path: '/patient-records' },
          { text: 'Lab Reports', icon: <DescriptionIcon />, path: '/lab-reports' },
          { text: 'Messages', icon: <MessageIcon />, path: '/messages' },
        ];
      case 'Admin':
        return [
          { text: 'Dashboard', icon: <DashboardIcon />, path: '/admin/dashboard' },
          { text: 'Users', icon: <PeopleIcon />, path: '/users' },
          { text: 'Messages', icon: <MessageIcon />, path: '/messages' },
        ];
      default:
        return [];
    }
  };

  return (
    <Box sx={{ display: 'flex' }}>
      <AppBar position="fixed">
        {/* ...existing toolbar code... */}
      </AppBar>
      <Drawer
        variant="permanent"
        sx={{
          width: 240,
          flexShrink: 0,
          '& .MuiDrawer-paper': { width: 240, boxSizing: 'border-box' },
        }}
      >
        <Toolbar />
        <List>
          {getNavItems().map((item) => (
            <ListItem button key={item.text} onClick={() => navigate(item.path)}>
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItem>
          ))}
        </List>
      </Drawer>
      <Box component="main" sx={{ flexGrow: 1, p: 3, mt: 8 }}>
        {children}
      </Box>
    </Box>
  );
};

export default Layout;