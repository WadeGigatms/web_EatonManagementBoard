import React from 'react'
import { Route, Switch } from 'react-router-dom'
import Layout from './components/Layout'
import Dashboard from './js/Dashboard'
import './css/custom.css'
import { VERSION } from './js/constants'

const App = () => {
	return <Layout version={VERSION}>
		<Switch>
			<Route exact path='/' component={Dashboard} />
		</Switch>
	</Layout>
}

export default App