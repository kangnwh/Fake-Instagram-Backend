package com.unimelb.mobile.configure;

import com.unimelb.mobile.dtos.UserCreateDTO;
import com.unimelb.mobile.orm.User;
import org.modelmapper.ModelMapper;
import org.modelmapper.PropertyMap;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Arrays;
import java.util.Date;

/**
 * DTOConfigure
 * <p>
 * Author Ning Kang
 * Date 11/9/18
 */

@Configuration
public class DTOConfigure {

	@Bean
	public ModelMapper modelMapper(){
		ModelMapper modelMapper = new ModelMapper();
		System.out.println("New mapper bean");

//		modelMapper.addMappings(createDtoTOUser);

		return modelMapper;
	};

//	PropertyMap<UserCreateDTO, User> createDtoTOUser = new PropertyMap<UserCreateDTO, User>() {
//		@Override
//		protected void configure() {
//
//			map().setCreateDate(new Date());
//		}
//	};


}
