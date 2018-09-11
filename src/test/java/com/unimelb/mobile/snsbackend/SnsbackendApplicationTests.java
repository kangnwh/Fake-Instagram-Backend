package com.unimelb.mobile.snsbackend;


import com.unimelb.mobile.controllers.PostController;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;

import static org.hamcrest.Matchers.equalTo;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;


@RunWith(SpringRunner.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class SnsbackendApplicationTests {

	private MockMvc mvc;

	@Before
	public void set() {
		mvc = MockMvcBuilders.standaloneSetup(new PostController()).build();
	}

	@Test
	public void homeTester() throws Exception{
		mvc.perform(MockMvcRequestBuilders.get("/").accept(MediaType.APPLICATION_JSON))
				.andExpect(status().isOk())
				.andExpect(content().string(equalTo("Hello World")));
	}

	@Test
	public void userTester() throws Exception{
//		mvc.perform(MockMvcRequestBuilders.get("/users/"))
//				.andExpect(status().isOk())
//				.andExpect(content().string(equalTo("[]")));

	}

}
