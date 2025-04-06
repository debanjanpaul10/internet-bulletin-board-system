import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import Cookies from "js-cookie";
import { Box, Button, Modal, Typography } from "@mui/material";

import {
	CookiesConstants,
	LoginPageConstants,
	modalStyle,
} from "@helpers/Constants";
import { GetUserAsync, ToggleLoginModal } from "@store/Users/Actions";
import Spinner from "@components/Common/Spinner";
import UserLoginDtoModel from "@models/UserLoginDto";

/**
 * @component
 * LoginComponent handles the user login functionality.
 *
 * @param {Object} props - The properties passed to the component.
 *
 * @returns {JSX.Element} The rendered component.
 */
function LoginComponent(props) {
	const dispatch = useDispatch();

	const UserStoreData = useSelector((state) => state.UsersReducer.userData);
	const UserErrorStoreData = useSelector(
		(state) => state.UsersReducer.userDataError
	);
	const IsDataLoading = useSelector(
		(state) => state.UsersReducer.isUserDataLoading
	);
	const IsLoginModalOpen = useSelector(
		(state) => state.UsersReducer.isLoginModalOpen
	);

	const [data, setData] = useState({
		UserEmail: "",
		UserPassword: "",
	});
	const [errors, setErrors] = useState({
		UserEmail: "",
		UserPassword: "",
	});
	const [errorState, setErrorState] = useState("");
	const [isModalOpen, setIsModalOpen] = useState(false);

	useEffect(() => {
		if (
			UserStoreData !== null &&
			UserStoreData !== undefined &&
			Object.keys(UserStoreData).length > 0
		) {
			handleModalCloseEvent();
			Cookies.set(
				CookiesConstants.LoggedInUser.Name,
				JSON.stringify(UserStoreData),
				{ expires: CookiesConstants.LoggedInUser.Timeout }
			);
		}
	}, [UserStoreData]);

	useEffect(() => {
		if (errorState !== UserErrorStoreData) {
			setErrorState(UserErrorStoreData);
		}
	}, [UserErrorStoreData]);

	useEffect(() => {
		if (IsLoginModalOpen !== isModalOpen) {
			setIsModalOpen(IsLoginModalOpen);
		}
	}, [IsLoginModalOpen]);

	/**
	 * Handles the form change event.
	 * @param {Event} event The on change event.
	 */
	const handleFormChange = (event) => {
		event.persist();
		const target = event.target;
		setData({
			...data,
			[target.name]: target.value,
		});
	};

	/**
	 * Handles the form submit event.
	 * @param {Event} event The submit event.
	 */
	const handleSubmit = (event) => {
		event.preventDefault();

		const validations = LoginPageConstants.validations;
		errors.UserEmail =
			data.UserEmail === "" ? validations.UserEmailRequired : "";
		errors.UserPassword =
			data.UserPassword === "" ? validations.UserPasswordRequired : "";
		setErrors({ ...errors });

		if (errors.UserEmail === "" && errors.UserPassword === "") {
			const loginData = new UserLoginDtoModel(
				data.UserEmail,
				data.UserPassword
			);

			dispatch(GetUserAsync(loginData));
		}
	};

	/**
	 * Handle the modal close event
	 */
	const handleModalCloseEvent = () => {
		dispatch(ToggleLoginModal(false));
	};

	return (
		<div className="container">
			<Spinner isLoading={IsDataLoading} />
			<Modal open={isModalOpen} onClose={handleModalCloseEvent}>
				<Box sx={modalStyle} className="custom-modal">
					<Typography
						id="modal-modal-title"
						variant="h6"
						component="h2"
					>
						<h1 className="architectDaughterfont text-center">
							{LoginPageConstants.Headings.LoginUser}
						</h1>
					</Typography>
					<Typography id="modal-modal-description" sx={{ mt: 2 }}>
						<form
							className="loginuser"
							style={{
								display: "flex",
								flexDirection: "column",
								alignItems: "center",
							}}
						>
							<div
								className="form-group row"
								style={{ width: "100%" }}
							>
								<div className="mb-3 mb-sm-0">
									<div className="row"></div>
									<div className="row p-2">
										<label
											htmlFor="UserEmail"
											className="form-label"
										>
											Email <span className="red">*</span>
										</label>
										<input
											type="email"
											name="UserEmail"
											onChange={handleFormChange}
											value={data.UserEmail}
											className="form-control mt-0 ml-10"
											id="UserEmail"
											placeholder="Email"
											style={{
												border: errors.UserEmail
													? "1px solid red"
													: "",
											}}
										/>
									</div>
								</div>
							</div>
							<div
								className="form-group row"
								style={{ width: "100%" }}
							>
								<div className="mb-3 mb-sm-0">
									<div className="row p-2 ">
										<label
											htmlFor="UserPassword"
											className="form-label"
										>
											Password{" "}
											<span className="red">*</span>
										</label>
										<input
											type="password"
											name="UserPassword"
											onChange={handleFormChange}
											value={data.UserPassword}
											className="form-control mt-0 ml-10"
											id="UserPassword"
											placeholder="Password"
											style={{
												border: errors.UserPassword
													? "1px solid red"
													: "",
											}}
										/>
									</div>
								</div>
							</div>

							<div
								className="text-center"
								style={{
									display: "flex",
									justifyContent: "center",
									gap: "10px",
								}}
							>
								<Button
									variant="contained"
									className="mt-3"
									onClick={handleSubmit}
								>
									{LoginPageConstants.Headings.LoginButton}
								</Button>
								&nbsp;
								<Button
									variant="contained"
									className="mt-3"
									color="error"
									onClick={handleModalCloseEvent}
								>
									{LoginPageConstants.Headings.CancelButton}
								</Button>
							</div>
						</form>
					</Typography>
				</Box>
			</Modal>
			<div className="row">
				<div className="col-sm-12 mt-5"></div>
			</div>
		</div>
	);
}

export default LoginComponent;
